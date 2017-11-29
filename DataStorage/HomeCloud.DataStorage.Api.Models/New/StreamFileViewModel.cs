namespace HomeCloud.DataStorage.Api.Models.New
{
	#region Usings

	using System.IO;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents file stream view model.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.New.FileViewModel" />
	public class StreamFileViewModel : FileViewModel
	{
		/// <summary>
		/// Gets or sets the file stream.
		/// </summary>
		/// <value>
		/// The file stream.
		/// </value>
		[JsonIgnore]
		public Stream Stream { get; set; }
	}
}
