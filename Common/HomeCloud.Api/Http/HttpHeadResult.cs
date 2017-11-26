namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Mvc;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	public class HttpHeadResult<T> : HttpGetResult<T>
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpHeadResult{T}"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpHeadResult(ControllerBase controller)
			: base(controller)
		{
		}

		#endregion

		#region HttpGetResult<T> Implementations

		/// <summary>
		/// Returns the <see cref="IActionResult" /> that represents the current <see cref="HTTP" /> method.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="IActionResult" />.
		/// </returns>
		public override IActionResult ToActionResult()
		{
			IActionResult result = null;
			if ((result = this.HandleError()) != null)
			{
				return result;
			}

			if (this.Data == null)
			{
				return this.Controller.NotFound();
			}

			Type type = this.Data.GetType();

			IEnumerable<PropertyInfo> properties = type.GetProperties().Where(property => property.GetCustomAttribute(typeof(HttpHeaderAttribute), true) != null);
			foreach (PropertyInfo property in properties)
			{
				string key = null;
				string value = null;

				if ((key = (property.GetCustomAttribute(typeof(HttpHeaderAttribute), true) as HttpHeaderAttribute)?.Name) != null)
				{
					if ((value = Convert.ToString(property.GetValue(this.Data))) != null)
					{
						this.Controller.HttpContext.Response.Headers[key] = value;
					}
				}
			}

			return this.Controller.Ok();
		}

		#endregion
	}
}
