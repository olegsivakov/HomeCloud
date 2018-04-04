namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System.IO;

	using HomeCloud.Http;
	using HomeCloud.Mvc.Models;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents file stream view model.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Models.IFileModel" />
	public class FileStreamViewModel : IFileModel
	{
		#region Constants

		/// <summary>
		/// The binary range
		/// </summary>
		private const string BinaryRange = "bytes";

		#endregion

		#region Public Properties

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
		[HttpHeader("Content-Type", HttpMethods.Head)]
		[JsonIgnore]
		public virtual string MimeType { get; set; }

		/// <summary>
		/// Gets the accept ranges.
		/// </summary>
		/// <value>
		/// The accept ranges.
		/// </value>
		[HttpHeader("Accept-Ranges", HttpMethods.Head)]
		[JsonIgnore]
		public virtual string AcceptRanges { get; } = BinaryRange;

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[HttpHeader("Content-Length", HttpMethods.Head)]
		[JsonIgnore]
		public virtual long Size { get; set; }

		/// <summary>
		/// Gets or sets the file stream.
		/// </summary>
		/// <value>
		/// The file stream.
		/// </value>
		[JsonIgnore]
		public Stream Stream { get; set; }

		#endregion
	}
}
