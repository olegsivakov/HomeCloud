namespace HomeCloud.Mvc.Formatters
{
	#region Usings

	using System;
	using System.IO;

	using Microsoft.Extensions.Primitives;
	using Microsoft.Net.Http.Headers;

	#endregion

	/// <summary>
	/// Provides helper methods to handle multipart requests.
	/// </summary>
	internal static class MultipartRequestHelper
	{
		/// <summary>
		/// Gets the multipart request boundary.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <param name="limit">The content limit.</param>
		/// <returns>The string representation of boundary.</returns>
		/// <exception cref="InvalidDataException">Missing content-type boundary.</exception>
		public static string GetBoundary(MediaTypeHeaderValue contentType, int limit)
		{
			StringSegment boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
			if (StringSegment.IsNullOrEmpty(boundary))
			{
				throw new InvalidDataException("Missing content-type boundary.");
			}

			if (boundary.Length > limit)
			{
				throw new InvalidDataException($"Multipart boundary length limit {limit} exceeded.");
			}

			return boundary.Value;
		}

		/// <summary>
		/// Determines whether the specified content type is multipart one.
		/// </summary>
		/// <param name="contentType">The content type string.</param>
		/// <returns>
		/// <c>true</c> if the specified content type is multipart one; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsMultipartContentType(string contentType)
		{
			return !string.IsNullOrEmpty(contentType)
				&& contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		/// <summary>
		/// Determines whether the specified instance of <see cref="ContentDispositionHeaderValue"/> has form data content disposition.
		/// </summary>
		/// <param name="contentDisposition">The content disposition header.</param>
		/// <returns>
		/// <c>true</c> if the specified instance of <see cref="ContentDispositionHeaderValue"/> has form data content disposition; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
		{
			return contentDisposition != null
				&& contentDisposition.DispositionType.Equals("form-data")
				&& StringSegment.IsNullOrEmpty(contentDisposition.FileName)
				&& StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar);
		}

		/// <summary>
		/// Determines whether the specified instance of <see cref="ContentDispositionHeaderValue"/> has file content disposition.
		/// </summary>
		/// <param name="contentDisposition">The content disposition header.</param>
		/// <returns>
		/// <c>true</c> if the specified instance of <see cref="ContentDispositionHeaderValue"/> has file content disposition; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
		{
			return contentDisposition != null
				&& contentDisposition.DispositionType.Equals("form-data")
				&& (!StringSegment.IsNullOrEmpty(contentDisposition.FileName)
				 || !StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar));
		}
	}
}
