namespace HomeCloud.Exceptions
{
	/// <summary>
	/// Represents the errors that occur when the resource cannot be found.
	/// </summary>
	/// <seealso cref="HomeCloud.Exceptions.ValidationException" />
	public class NotFoundException : ValidationException
	{
		#region Constants

		/// <summary>
		/// The <see cref="'The requested resource cannot be found.'"/> message
		/// </summary>
		private const string NotFoundExceptionMessage = "The requested resource cannot be found.";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public NotFoundException(string message = null)
			: base(message ?? NotFoundExceptionMessage)
		{
		}

		#endregion
	}
}
