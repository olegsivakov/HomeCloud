namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;

	using HomeCloud.Api.Http;
	using HomeCloud.Api.Mvc;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents binary file view model.
	/// </summary>
	/// <seealso cref="HomeCloud.Api.Mvc.IFileViewModel" />
	public class FileViewModel : IFileViewModel
	{
		#region Constants

		/// <summary>
		/// The binary range
		/// </summary>
		private const string BinaryRange = "bytes";

		#endregion

		#region Public Properties

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
		public virtual string FileName { get; set; }

		/// <summary>
		/// Gets or sets the physical path to the binary.
		/// </summary>
		/// <value>
		/// The physical path to the binary.
		/// </value>
		[JsonIgnore]
		public virtual string Path { get; set; }

		/// <summary>
		/// Gets or sets the MIME type.
		/// </summary>
		/// <value>
		/// The MIME type.
		/// </value>
		[HttpHeader("Content-Type")]
		[JsonIgnore]
		public virtual string MimeType { get; set; }

		/// <summary>
		/// Gets the accept ranges.
		/// </summary>
		/// <value>
		/// The accept ranges.
		/// </value>
		[HttpHeader("Accept-Ranges")]
		[JsonIgnore]
		public virtual string AcceptRanges { get; } = BinaryRange;

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[HttpHeader("Content-Length")]
		[JsonIgnore]
		public virtual long Size { get; set; }

		#endregion
	}
}
