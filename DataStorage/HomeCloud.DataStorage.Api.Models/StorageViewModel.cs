namespace HomeCloud.DataStorage.Api.Models
{
	using Newtonsoft.Json;
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents storage view model.
	/// </summary>
	public class StorageViewModel
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
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public virtual long Size { get; set; }

		/// <summary>
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long? Quota { get; set; }
	}
}
