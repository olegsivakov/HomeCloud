namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents storage entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.CatalogRoot" />
	/// <seealso cref="System.IComparable{HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="System.ICloneable" />
	public class Storage : CatalogRoot, IComparable<Storage>, ICloneable
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long? Quota { get; set; }

		#endregion

		#region IComparable<Storage> Implementations

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		public int CompareTo(Storage other)
		{
			if (!string.IsNullOrWhiteSpace(other.DisplayName) && this.DisplayName != other.DisplayName)
			{
				return 1;
			}

			return 0;
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
			return this.MemberwiseClone();
		}

		#endregion
	}
}
