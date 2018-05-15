namespace HomeCloud.IdentityService.Api.Models
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.Mvc.Models;

	#endregion

	/// <summary>
	/// Represents a string collection of application related data such as secrets, scopes, claims etc.
	/// </summary>
	public class StringListViewModel : PagedListViewModel<string>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StringListViewModel"/> class.
		/// </summary>
		/// <param name="applicationID">The application identifier.</param>
		public StringListViewModel(Guid applicationID)
			: base()
		{
			this.ApplicationID = applicationID;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StringListViewModel"/> class.
		/// </summary>
		/// <param name="items">The <see cref="StringListViewModel"/> item collection.</param>
		/// <param name="applicationID">The application identifier.</param>
		public StringListViewModel(IEnumerable<string> items, Guid applicationID)
			: base(items)
		{
			this.ApplicationID = applicationID;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the application identifier.
		/// </summary>
		/// <value>
		/// The application identifier.
		/// </value>
		public Guid ApplicationID { get; set; }

		#endregion
	}
}
