namespace HomeCloud.Exceptions
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the errors that occur when the party is not authenticated to access to the requested resource.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class NotAuthenticatedException : Exception
	{
		#region Constants

		/// <summary>
		/// The <see cref="'The anonymous access to the requested resource is denied.'"/> message
		/// </summary>
		private const string NotAuthenticatedExceptionMessage = "The anonymous access to the requested resource is denied.";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NotAuthenticatedException"/> class.
		/// </summary>
		public NotAuthenticatedException()
			: base(NotAuthenticatedExceptionMessage)
		{
		}

		#endregion
	}
}
