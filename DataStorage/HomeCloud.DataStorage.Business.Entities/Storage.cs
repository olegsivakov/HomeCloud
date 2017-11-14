namespace HomeCloud.DataStorage.Business.Entities
{
	/// <summary>
	/// Represents storage entity.
	/// </summary>
	public class Storage : CatalogRoot
	{
		#region Public Properties

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
