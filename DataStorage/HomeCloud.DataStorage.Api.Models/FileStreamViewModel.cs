namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System.IO;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents file stream view model.
	/// </summary>
	public class FileStreamViewModel
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
