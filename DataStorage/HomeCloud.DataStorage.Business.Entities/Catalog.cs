namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents catalog entity.
	/// </summary>
	public class Catalog : CatalogRoot, ICloneable
	{
		/// <summary>
		/// Gets or sets the parent catalog.
		/// </summary>
		/// <value>
		/// The parent catalog.
		/// </value>
		public CatalogRoot Parent { get; set; }

		#region ICloneable Impolementations

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public override object Clone()
		{
			Catalog catalog = this.MemberwiseClone() as Catalog;
			catalog.Parent = this.Parent?.Clone() as CatalogRoot;

			return catalog;
		}

		#endregion
	}
}
