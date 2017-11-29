namespace HomeCloud.DataStorage.Api.Models.New
{
	#region Usings

	using HomeCloud.Api.Mvc;
	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents catalog entry view model.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.New.DataViewModel" />
	public class CatalogEntryViewModel : DataViewModel
	{
		/// <summary>
		/// Gets or sets the MIME type.
		/// </summary>
		/// <value>
		/// The MIME type.
		/// </value>
		public string MimeType { get; set; }
	}
}
