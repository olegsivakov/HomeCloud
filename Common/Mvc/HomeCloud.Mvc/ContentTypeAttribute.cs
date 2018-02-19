namespace HomeCloud.Mvc
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
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public class ContentTypeAttribute : Attribute, IActionConstraint
	{
		#region Constants

		/// <summary>
		/// The content type header name
		/// </summary>
		private const string ContentTypeHeaderName = "Content-Type";

		#endregion

		#region Private Members

		/// <summary>
		/// The list of accepted content types
		/// </summary>
		private readonly IEnumerable<string> contentTypes = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ContentTypeAttribute" /> class.
		/// </summary>
		/// <param name="contentTypes">The content types.</param>
		public ContentTypeAttribute(params string[] contentTypes)
		{
			this.contentTypes = contentTypes ?? Enumerable.Empty<string>();
		}


		#endregion

		#region Public Properties

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

			return this.contentTypes.Any(contentType => (request.Headers[ContentTypeHeaderName].ToString()).ToLower().Contains(contentType.ToLower()));
		}

		#endregion
	}
}
