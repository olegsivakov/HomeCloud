namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	using Microsoft.AspNetCore.StaticFiles;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="FileViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.Converters.DataViewModelConverter" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.FileViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.FileViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class FileViewModelConverter : DataViewModelConverter, ITypeConverter<CatalogEntry, FileViewModel>, ITypeConverter<FileViewModel, CatalogEntry>
	{
		#region Constants

		/// <summary>
		/// The "<see cref="application/octet-stream"/>" MIME type
		/// </summary>
		private const string DefaultMimeType = "application/octet-stream";

		#endregion

		#region Private Members

		/// <summary>
		/// The <see cref="IContentTypeProvider"/> provider.
		/// </summary>
		private IContentTypeProvider contentTypeProvider = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileViewModelConverter"/> class.
		/// </summary>
		/// <param name="contentTypeProvider">The <see cref="IContentTypeProvider"/> provider.</param>
		public FileViewModelConverter(IContentTypeProvider contentTypeProvider)
		{
			this.contentTypeProvider = contentTypeProvider;
		}

		#endregion

		#region ITypeConverter<CatalogEntry, FileViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public FileViewModel Convert(CatalogEntry source, FileViewModel target)
		{
			this.Convert(source, (DataViewModel)target);

			target.FileName = source.Name;
			target.Path = source.Path;
			target.MimeType = this.GetContentType(target.Path);

			return target;
		}

		#endregion

		#region ITypeConverter<FileViewModel, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(FileViewModel source, CatalogEntry target)
		{
			this.Convert((DataViewModel)source, target);

			target.Name = source.FileName;
			target.Path = source.Path;

			return target;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the content type determined by specified file path.
		/// </summary>
		/// <param name="path">The file path.</param>
		/// <returns>The content type. If the content type cannot be determined the default "<see cref="application/octet-stream"/>" will be returned.</returns>
		private string GetContentType(string path)
		{
			string contentType = null;
			if ((this.contentTypeProvider?.TryGetContentType(path, out contentType)).GetValueOrDefault())
			{
				return contentType;
			}

			return DefaultMimeType;
		}

		#endregion
	}
}
