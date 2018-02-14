namespace HomeCloud.Mvc.Exceptions
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents <see cref="HTTP"/>-based error response.
	/// </summary>
	public class HttpExceptionResponse
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; } = Guid.NewGuid();

		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public virtual int StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the error messages.
		/// </summary>
		/// <value>
		/// The error messages.
		/// </value>
		public virtual IEnumerable<string> Errors { get; set; }
	}
}
