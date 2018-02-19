namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the paged list of data.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.PagedListViewModel{HomeCloud.DataStorage.Api.Models.DataViewModel}" />
	public class DataListViewModel : PagedListViewModel<DataViewModel>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataListViewModel"/> class.
		/// </summary>
		/// <param name="catalogID">The catalog identifier.</param>
		public DataListViewModel(Guid catalogID)
			: base()
		{
			this.CatalogID = catalogID;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogListViewModel"/> class.
		/// </summary>
		/// <param name="items">The <see cref="DataListViewModel"/> item collection.</param>
		/// <param name="catalogID">The catalog identifier.</param>
		public DataListViewModel(IEnumerable<DataViewModel> items, Guid catalogID)
			: base(items)
		{
			this.CatalogID = catalogID;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the catalog identifier.
		/// </summary>
		/// <value>
		/// The catalog identifier.
		/// </value>
		public Guid CatalogID { get; set; }

		#endregion
	}
}
