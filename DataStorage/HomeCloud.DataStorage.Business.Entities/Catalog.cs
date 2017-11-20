namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents catalog entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.CatalogRoot" />
	/// <seealso cref="System.IComparable{HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="System.ICloneable" />
	public class Catalog : CatalogRoot, IComparable<Catalog>, ICloneable
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the parent catalog.
		/// </summary>
		/// <value>
		/// The parent catalog.
		/// </value>
		public CatalogRoot Parent { get; set; }

		#endregion

		#region IComparable<Catalog> Implementations

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		public int CompareTo(Catalog other)
		{
			int result = base.CompareTo(other);

			if ((other.Parent?.ID).HasValue && this.Parent?.ID != other.Parent?.ID)
			{
				return result | 1;
			}

			return result;
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
			catalog.Parent = this.Parent?.Clone() as CatalogRoot;

			return catalog;
		}

		#endregion
	}
}
