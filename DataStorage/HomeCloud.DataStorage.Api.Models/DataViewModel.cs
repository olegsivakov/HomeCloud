namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents data view model.
	/// </summary>
	public class DataViewModel
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
		/// Gets or sets a value indicating whether this instance is catalog.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is catalog; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsCatalog { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the data exists in file system.
		/// </summary>
		/// <value>
		///   <c>true</c> if the data exists; otherwise, it returns <c>false</c>.
		/// </value>
		public virtual bool Exists { get; set; }
	}
}
