namespace HomeCloud.Api.Mvc
{
	using System.Collections.Generic;
	#region Usings

	using HomeCloud.Exceptions;

	#endregion

	/// <summary>
	/// Represents error view model.
	/// </summary>
	/// <seealso cref="HomeCloud.Api.Mvc.IViewModel" />
	public class ErrorViewModel : HttpExceptionResponse, IViewModel
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorViewModel"/> class.
		/// </summary>
		public ErrorViewModel()
			: base()
		{
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the error messages.
		/// </summary>
		/// <value>
		/// The error messages.
		/// </value>
		public override IEnumerable<string> Errors { get => base.Errors; set => base.Errors = value; }

		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public override int StatusCode { get => base.StatusCode; set => base.StatusCode = value; }

		#endregion
	}
}
