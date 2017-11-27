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

		private const string DefaultMimeType = "application/octet-stream";

		private const string BinaryRange = "bytes";

		#endregion

		public string Path { get; set ; }

		[HttpHeader("Content-Type")]
		[JsonIgnore]
		public string MimeType { get; set; } = DefaultMimeType;

		[HttpHeader("Accept-Ranges")]
		[JsonIgnore]
		public string AcceptRanges { get; } = BinaryRange;

		[HttpHeader("Content-Length")]
		public new long Size { get; set; }
	}
}
