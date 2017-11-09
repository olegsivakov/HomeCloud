namespace HomeCloud.Exceptions
{
	/// <summary>
	/// Represents the errors that occur when the resource already exists.
	/// </summary>
	/// <seealso cref="HomeCloud.Exceptions.ValidationException" />
	public class AlreadyExistsException : ValidationException
	{
		#region Constants

		/// <summary>
		/// The <see cref="'The requested resource cannot be found.'"/> message
		/// </summary>
		private const string NotFoundExceptionMessage = "The requested resource already exists.";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AlreadyExistsException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AlreadyExistsException(string message = null)
			: base(message ?? NotFoundExceptionMessage)
		{
		}

		#endregion
	}
}
