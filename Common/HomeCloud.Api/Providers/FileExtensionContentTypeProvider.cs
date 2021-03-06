﻿namespace HomeCloud.Api.Providers
{
	#region Usings

	using System.Diagnostics.CodeAnalysis;

	#endregion

	/// <summary>
	/// Provides a mapping between file extensions and MIME types.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider" />
	/// <seealso cref="HomeCloud.Api.Providers.IContentTypeProvider" />
	public class FileExtensionContentTypeProvider : Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider, IContentTypeProvider
	{
		#region Constants

		/// <summary>
		/// The "<see cref="application/octet-stream"/>" MIME type
		/// </summary>
		private const string DefaultMimeType = "application/octet-stream";

		#endregion

		/// <summary>
		/// Determines the content type by the specified file path. If content type is not found it returns <see cref="application/octet-stream"/> content type.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <returns>The MIME type.</returns>
		[SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "C# 7.0 syntax not supported")]
		public string GetContentType(string path)
		{
			if (this.TryGetContentType(path, out string contentType))
			{
				return contentType;
			}

			return DefaultMimeType;
		}
	}
}
