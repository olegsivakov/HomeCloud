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
	public class ApplicationDataListViewModel<T> : PagedListViewModel<T>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDataListViewModel"/> class.
		/// </summary>
		/// <param name="applicationID">The application identifier.</param>
		public ApplicationDataListViewModel(Guid applicationID)
			: base()
		{
			this.ApplicationID = applicationID;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationDataListViewModel"/> class.
		/// </summary>
		/// <param name="items">The <see cref="ApplicationDataListViewModel"/> item collection.</param>
		/// <param name="applicationID">The application identifier.</param>
		public ApplicationDataListViewModel(IEnumerable<T> items, Guid applicationID)
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
