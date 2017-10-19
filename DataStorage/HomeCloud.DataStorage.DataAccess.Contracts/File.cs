namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents <see cref="File"/> data contract.
	/// </summary>
	public class File
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the directory identifier.
		/// </summary>
		/// <value>
		/// The directory identifier.
		/// </value>
		public Guid DirectoryID {get;set;}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the extension.
		/// </summary>
		/// <value>
		/// The extension.
		/// </value>
		public string Extension { get; set; }

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
