namespace HomeCloud.Mvc
{
	#region Usings

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// An <see cref=" Microsoft.AspNetCore.Mvc.ActionResult"/> that returns a <see cref="StatusCodes.Status201Created"/> response with a <see cref="Location"/> header.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.CreatedResult" />
	public class CreatedObjectResult : ObjectResult
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="CreatedObjectResult" /> class.
		/// </summary>
		/// <param name="value">The content to format into the entity body.</param>
		public CreatedObjectResult(object value)
			: base(value)
		{
			this.StatusCode = StatusCodes.Status201Created;
		}
	}
}
