namespace HomeCloud.Api.Http
{
	#region Usings

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// An Microsoft.AspNetCore.Mvc.ObjectResult that when executed performs content
	/// negotiation, formats the entity body, and will produce a <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict"/>
	/// response if negotiation and formatting succeed.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ObjectResult" />
	public class ConflictResult : ObjectResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConflictResult" /> class.
		/// </summary>
		/// <param name="value">The content to format into the entity body.</param>
		public ConflictResult(object value)
			: base(value)
		{
			this.StatusCode = StatusCodes.Status409Conflict;
		}
	}
}
