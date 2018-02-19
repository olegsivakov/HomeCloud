namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the paged list of catalogs.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.PagedListViewModel{HomeCloud.DataStorage.Api.Models.CatalogViewModel}" />
	public class CatalogListViewModel : PagedListViewModel<CatalogViewModel>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogListViewModel"/> class.
		/// </summary>
		/// <param name="parentID">The parent catalog identifier.</param>
		public CatalogListViewModel(Guid parentID)
			: base()
		{
			this.ParentID = parentID;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogListViewModel"/> class.
		/// </summary>
		/// <param name="items">The <see cref="CatalogViewModel"/> item collection.</param>
		/// <param name="parentID">The parent identifier.</param>
		public CatalogListViewModel(IEnumerable<CatalogViewModel> items, Guid parentID)
			: base(items)
		{
			this.ParentID = parentID;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the parent catalog identifier.
		/// </summary>
		/// <value>
		/// The parent catalog identifier.
		/// </value>
		public Guid ParentID { get; set; }

		#endregion
	}
}
