namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// An Microsoft.AspNetCore.Mvc.ObjectResult that when executed performs content
	/// negotiation, formats the entity body, and will produce a Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity
	/// response if negotiation and formatting succeed.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
	public class UnprocessableEntityResult : ObjectResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnprocessableEntityResult" /> class.
		/// </summary>
		/// <param name="value">The content to format into the entity body.</param>
		public UnprocessableEntityResult(object value)
			: base(value)
		{
			this.StatusCode = StatusCodes.Status422UnprocessableEntity;
		}
	}
}
