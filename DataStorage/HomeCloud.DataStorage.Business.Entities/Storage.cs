namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents storage entity.
	/// </summary>
	public class Storage
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long Quota { get; set; }

		/// <summary>
		/// Gets or sets the root catalog in file storage.
		/// </summary>
		/// <value>
		/// The root catalog.
		/// </value>
		public Catalog CatalogRoot { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		public DateTime UpdatedDate { get; set; }
	}
}
