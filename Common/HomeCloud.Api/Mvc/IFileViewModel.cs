namespace HomeCloud.Api.Mvc
{
	/// <summary>
	/// Defines the binary file view model.
	/// </summary>
	/// <seealso cref="HomeCloud.Api.Mvc.IViewModel" />
	public interface IFileViewModel : IViewModel
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string FileName { get; set; }

		/// <summary>
		/// Gets or sets the physical path to the binary.
		/// </summary>
		/// <value>
		/// The physical path to the binary.
		/// </value>
		string Path { get; set; }

		/// <summary>
		/// Gets or sets the MIME type.
		/// </summary>
		/// <value>
		/// The MIME type.
		/// </value>
		string MimeType { get; set; }
	}
}
