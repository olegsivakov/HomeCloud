namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents catalog entity.
	/// </summary>
	public class Catalog
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the parent catalog identifier.
		/// </summary>
		/// <value>
		/// The parent catalog identifier.
		/// </value>
		public Guid? ParentID { get; set; }

		/// <summary>
		/// Gets or sets the storage identifier.
		/// </summary>
		/// <value>
		/// The storage identifier.
		/// </value>
		public Guid StorageID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the catalog path.
		/// </summary>
		/// <value>
		/// The catalog path.
		/// </value>
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets the file size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public long Size { get; set; }

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
