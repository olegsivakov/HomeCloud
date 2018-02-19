namespace HomeCloud.Mvc.Formatters
{
	#region Usings

	using System;
	using System.Buffers;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Formatters;

	using Newtonsoft.Json;

	using HomeCloud.Core.Extensions;
	using HomeCloud.Http;

	#endregion

	/// <summary>
	/// An extended <see cref="Microsoft.AspNetCore.Mvc.Formatters.JsonOutputFormatter"/> for JSON content.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Formatters.JsonOutputFormatter" />
	public class JsonOutputFormatter : Microsoft.AspNetCore.Mvc.Formatters.JsonOutputFormatter
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonOutputFormatter"/> class.
		/// </summary>
		/// <param name="serializerSettings">The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />. Should be either the application-wide settings
		/// (<see cref="P:Microsoft.AspNetCore.Mvc.MvcJsonOptions.SerializerSettings" />) or an instance
		/// <see cref="M:Microsoft.AspNetCore.Mvc.Formatters.JsonSerializerSettingsProvider.CreateSerializerSettings" /> initially returned.</param>
		/// <param name="charPool">The <see cref="T:System.Buffers.ArrayPool`1" />.</param>
		public JsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool)
			: base(serializerSettings, charPool)
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Writes the response body.
		/// </summary>
		/// <param name="context">The formatter context associated with the call.</param>
		/// <param name="selectedEncoding">Encoding.</param>
		/// <returns>
		/// A task which can write the response body.
		/// </returns>
		/// <inheritdoc />
		public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
		{
			await base.WriteResponseBodyAsync(context, selectedEncoding);
		}

		/// <summary>
		/// Sets the headers on <see cref="T:Microsoft.AspNetCore.Http.HttpResponse" /> object.
		/// </summary>
		/// <param name="context">The formatter context associated with the call.</param>
		public override void WriteResponseHeaders(OutputFormatterWriteContext context)
		{
			base.WriteResponseHeaders(context);

			WriteResponseHeaders(context.HttpContext.Response, context.Object);
		}

		#endregion

		#region Private Methods

		private static void WriteResponseHeaders(HttpResponse response, object obj)
		{
			IEnumerable<PropertyInfo> properties = obj?.GetType().GetProperties().Where(property => (property.GetIndexParameters()?.Length).GetValueOrDefault() == 0) ?? Enumerable.Empty<PropertyInfo>();
			foreach (PropertyInfo property in properties)
			{
				HttpHeaderAttribute headerAttribute = property.GetCustomAttribute(typeof(HttpHeaderAttribute), true) as HttpHeaderAttribute;
				if (headerAttribute != null && headerAttribute.AllowedHttpMethods.Contains(response.HttpContext.Request.Method.ToUpper()))
				{
					response.Headers[headerAttribute.Name] = Convert.ToString(property.GetValue(obj));
				}

				if (!property.PropertyType.IsPrimitive())
				{
					object propertyValue = property.GetValue(obj);
					WriteResponseHeaders(response, propertyValue);
				}
			}
		}

		#endregion
	}
}
