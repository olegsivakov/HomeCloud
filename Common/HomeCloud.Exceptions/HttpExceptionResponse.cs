namespace HomeCloud.Exceptions
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents <see cref="HTTP"/>-based error response.
	/// </summary>
	public class HttpExceptionResponse
	{
		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public int StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the error messages.
		/// </summary>
		/// <value>
		/// The error messages.
		/// </value>
		public IEnumerable<string> Errors { get; set; }
	}
}
