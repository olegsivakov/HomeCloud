namespace HomeCloud.Mvc.Providers
{
	/// <summary>
	/// Defines methods to map file extensions and MIME types.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.StaticFiles.IContentTypeProvider" />
	public interface IContentTypeProvider : Microsoft.AspNetCore.StaticFiles.IContentTypeProvider
	{
		/// <summary>
		/// Determines the content type by the specified file path. If content type is not found it returns the default content type.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <returns>The MIME type.</returns>
		string GetContentType(string path);
	}
}
