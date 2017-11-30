namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents catalog entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.CatalogRoot" />
	/// <seealso cref="System.ICloneable" />
	public class Catalog : CatalogRoot, ICloneable
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the parent catalog.
		/// </summary>
		/// <value>
		/// The parent catalog.
		/// </value>
		public Catalog Parent { get; set; }

		#endregion

		#region Public Overloads

		/// <summary>
		/// Performs an explicit conversion from <see cref="Storage"/> to <see cref="Catalog"/>.
		/// </summary>
		/// <param name="storage">The storage.</param>
		/// <returns>
		/// The result of the conversion.
		/// </returns>
		public static explicit operator Catalog(Storage storage)
		{
			return new Catalog()
			{
				ID = storage.ID,
				Name = storage.Name,
				CreationDate = storage.CreationDate,
				UpdatedDate = storage.UpdatedDate,
				Path = storage.Path,
				Size = storage.Size
			};
		}

		#endregion

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
			catalog.Parent = this.Parent?.Clone() as Catalog;

			return catalog;
		}

		#endregion
	}
}
