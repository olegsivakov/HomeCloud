namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents binary file view model.
	/// </summary>
	public class FileViewModel : DataViewModel
	{
		#region Constants

		/// <summary>
		/// The binary range
		/// </summary>
		private const string BinaryRange = "bytes";

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the MIME type.
		/// </summary>
		/// <value>
		/// The MIME type.
		/// </value>
		public virtual string MimeType { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public virtual long Size { get; set; }

		#endregion
	}
}
