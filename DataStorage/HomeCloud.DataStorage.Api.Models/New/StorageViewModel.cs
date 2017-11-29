namespace HomeCloud.DataStorage.Api.Models.New
{
	#region Usings

	using System;

	using HomeCloud.Api.Mvc;

	#endregion

	/// <summary>
	/// Represents storage view model.
	/// </summary>
	public class StorageViewModel : DataViewModel
	{
		/// <summary>
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long? Quota { get; set; }
	}
}
