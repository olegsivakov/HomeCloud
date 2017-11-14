namespace HomeCloud.DataStorage.Business.Entities
{
	/// <summary>
	/// Represents catalog entity.
	/// </summary>
	public class Catalog : CatalogRoot
	{
		/// <summary>
		/// Gets or sets the parent catalog.
		/// </summary>
		/// <value>
		/// The parent catalog.
		/// </value>
		public Catalog Parent { get; set; }
	}
}
