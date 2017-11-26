namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// An <see cref="Microsoft.AspNetCore.Mvc.ObjectResult"/> that when executed performs content
	/// negotiation, formats the entity body, and will produce a <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status206PartialContent"/>
	/// response if negotiation and formatting succeed.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
	public class PartialContentResult : ObjectResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PartialContentResult" /> class.
		/// </summary>
		/// <param name="value">The content to format into the entity body.</param>
		public PartialContentResult(object value)
			: base(value)
		{
			this.StatusCode = StatusCodes.Status206PartialContent;
		}
	}
}
