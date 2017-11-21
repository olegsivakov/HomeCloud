namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents storage entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.CatalogRoot" />
	/// <seealso cref="System.ICloneable" />
	public class Storage : CatalogRoot, ICloneable
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
