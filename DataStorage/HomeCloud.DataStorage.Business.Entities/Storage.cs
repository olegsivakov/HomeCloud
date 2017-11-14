namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents storage entity.
	/// </summary>
	public class Storage : CatalogRoot
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the storage identifier.
		/// </summary>
		/// <value>
		/// The storage identifier.
		/// </value>
		public new Guid StorageID
		{
			get => this.ID;
			set => this.ID = value;
		}

		/// <summary>
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long? Quota { get; set; }

		#endregion
	}
}
