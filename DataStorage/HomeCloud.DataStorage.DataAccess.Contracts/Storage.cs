namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;
	using Dapper.Contrib.Extensions;

	#endregion

	/// <summary>
	/// Represents <see cref="Storage" /> data contract.
	/// </summary>
	public class Storage
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[Write(false)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long Quota { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		[Write(false)]
		[Computed]
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		[Write(false)]
		[Computed]
		public DateTime UpdatedDate { get; set; }
	}
}
