namespace HomeCloud.Exceptions
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the errors occurred during object validation.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class ValidationException : Exception
	{
		#region constants

		/// <summary>
		/// The exception message.
		/// </summary>
		private const string ValidationExceptionMessge = "The instance of an object is not valid.";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ValidationException(string message = null)
			: base(message ?? ValidationExceptionMessge)
		{
		}

		#endregion
	}
}
