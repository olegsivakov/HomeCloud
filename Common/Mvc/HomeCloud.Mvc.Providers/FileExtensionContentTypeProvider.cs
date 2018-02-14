namespace HomeCloud.Mvc.Providers
{
	#region Usings

	using HomeCloud.Http;

	#endregion

	/// <summary>
	/// Provides a mapping between file extensions and MIME types.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Providers.IContentTypeProvider" />
	/// <seealso cref="Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider" />
	public class FileExtensionContentTypeProvider : Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider, IContentTypeProvider
	{
		/// <summary>
		/// Determines the content type by the specified file path. If content type is not found it returns <see cref="application/octet-stream"/> content type.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <returns>The MIME type.</returns>
		public string GetContentType(string path)
		{
			if (this.TryGetContentType(path, out string contentType))
			{
				return contentType;
			}

			return MimeTypes.Application.OctetStream;
		}
	}
}
