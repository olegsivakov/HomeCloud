namespace HomeCloud.Exceptions
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the errors occured during object validation.
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
		/// Initializes a new instance of the <see cref="ValidationException"/> class.
		/// </summary>
		/// <param name="errors">The list of validation errors.</param>
		public ValidationException(IEnumerable<string> errors)
			: base(ValidationExceptionMessge)
		{
			this.Errors = errors;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the list of validation errors.
		/// </summary>
		/// <value>
		/// The list of validation errors.
		/// </value>
		public IEnumerable<string> Errors { get; private set; }

		#endregion
	}
}
