namespace HomeCloud.Mvc.ActionConstraints
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.ActionConstraints;

	#endregion

	/// <summary>
	/// Accepts and requires the action method to process the request only for specified <see cref="Content-Type"/> header values.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint" />
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
	public class ContentTypeAttribute : Attribute, IActionConstraint
	{
		#region Constants

		/// <summary>
		/// The content type header name
		/// </summary>
		private const string ContentTypeHeaderName = "Content-Type";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentTypeAttribute" /> class.
		/// </summary>
		/// <param name="contentType">The content type.</param>
		public ContentTypeAttribute(string contentType)
		{
			if (string.IsNullOrWhiteSpace(contentType))
			{
				throw new ArgumentNullException(nameof(contentType));
			}

			this.ContentType = contentType.ToLower();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the accepted content type.
		/// </summary>
		/// <value>
		/// The accepted content type.
		/// </value>
		public string ContentType { get; private set; }

		/// <summary>
		/// Gets the constraint order.
		/// </summary>
		/// <remarks>
		/// Constraints are grouped into stages by the value of <see cref="P:Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint.Order" />. See remarks on
		/// <see cref="T:Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraint" />.
		/// </remarks>
		public int Order => 0;

		#endregion

		#region Public Methods
		/// <summary>
		/// Determines whether an action is a valid candidate for selection.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ActionConstraints.ActionConstraintContext" />.</param>
		/// <returns>
		/// True if the action is valid for selection, otherwise false.
		/// </returns>
		public bool Accept(ActionConstraintContext context)
		{
			HttpRequest request = context.RouteContext.HttpContext.Request;

			if (!request.Headers.ContainsKey(ContentTypeHeaderName))
				return false;

			return this.ContentType.ToLower() == Convert.ToString(request.Headers[ContentTypeHeaderName])?.ToLower();
		}

		#endregion
	}
}
