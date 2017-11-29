namespace HomeCloud.Api.Providers
{
	/// <summary>
	/// Defines methods to map file extensions and MIME types.
	/// </summary>
	public interface IContentTypeProvider : Microsoft.AspNetCore.StaticFiles.IContentTypeProvider
	{
		/// <summary>
		/// Determines the content type by the specified file path. If content type is not found it returns <see cref="application/octet-stream"/> content type.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <returns>The MIME type.</returns>
		string GetContentType(string path);
	}
}
