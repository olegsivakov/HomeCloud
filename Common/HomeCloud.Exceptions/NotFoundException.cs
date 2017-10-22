namespace HomeCloud.Exceptions
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the errors that occur when the resource cannot be found.
	/// </summary>
	/// <seealso cref="System.Exception" />
	public class NotFoundException : Exception
	{
		#region Constants

		/// <summary>
		/// The <see cref="'The requested resource cannot be found.'"/> message
		/// </summary>
		private const string NotFoundExceptionMessage = "The requested resource cannot be found.";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException"/> class.
		/// </summary>
		public NotFoundException()
			: base(NotFoundExceptionMessage)
		{
		}

		#endregion
	}
}
