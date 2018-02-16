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
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the data exists in file system.
		/// </summary>
		/// <value>
		///   <c>true</c> if the data exists; otherwise, it returns <c>false</c>.
		/// </value>
		public virtual bool IsExists { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public virtual long Size { get; set; }

		/// <summary>
		/// Gets or sets the MIME type.
		/// </summary>
		/// <value>
		/// The MIME type.
		/// </value>
		public virtual string MimeType { get; set; }
	}
}
