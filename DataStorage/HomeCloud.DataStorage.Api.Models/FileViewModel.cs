namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using HomeCloud.Api.Http;
	using HomeCloud.Api.Mvc;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents binary file view model.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.DataViewModel" />
	/// <seealso cref="HomeCloud.Api.Mvc.IFileViewModel" />
	public class FileViewModel : DataViewModel, IFileViewModel
	{
		#region Constants

		/// <summary>
		/// The binary range
		/// </summary>
		private const string BinaryRange = "bytes";

		#endregion

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>
		/// The name of the file.
		/// </value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the physical path to the binary.
		/// </summary>
		/// <value>
		/// The physical path to the binary.
		/// </value>
		public string Path { get; set; }

		public System.IO.Stream Stream { get; set; }

		/// <summary>
		/// Gets or sets the MIME type.
		/// </summary>
		/// <value>
		/// The MIME type.
		/// </value>
		[HttpHeader("Content-Type")]
		[JsonIgnore]
		public string MimeType { get; set; }

		/// <summary>
		/// Gets the accept ranges.
		/// </summary>
		/// <value>
		/// The accept ranges.
		/// </value>
		[HttpHeader("Accept-Ranges")]
		[JsonIgnore]
		public string AcceptRanges { get; } = BinaryRange;

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[HttpHeader("Content-Length")]
		public new long Size
		{
			get => base.Size;
			set => base.Size = value;
		}
	}
}
