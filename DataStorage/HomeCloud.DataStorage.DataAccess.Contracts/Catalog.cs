﻿namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents <see cref="Catalog" /> data contract.
	/// </summary>
	public class Catalog
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the parent identifier.
		/// </summary>
		/// <value>
		/// The parent identifier.
		/// </value>
		public Guid? ParentID { get; set; }

		/// <summary>
		/// Gets or sets the storage identifier.
		/// </summary>
		/// <value>
		/// The storage identifier.
		/// </value>
		public Guid StorageID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		public DateTime UpdatedDate { get; set; }
	}
}