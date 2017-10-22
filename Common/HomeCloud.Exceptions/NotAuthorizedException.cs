namespace HomeCloud.Exceptions
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the errors that occur when the access to the requested resource is denied for requested party.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class NotAuthorizedException : Exception
	{
		#region Constants

		/// <summary>
		/// The <see cref="'The access to the requested resource is not authorized for the requested party.'"/> message
		/// </summary>
		private const string NotAuthorizedExceptionMessage = "The access to the requested resource is not authorized for the requested party.";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NotAuthorizedException"/> class.
		/// </summary>
		public NotAuthorizedException()
			: base(NotAuthorizedExceptionMessage)
		{
		}

		#endregion
	}
}
