namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Api.Providers;
	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="FileStreamViewModelConverter" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.PhysicalFileViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.PhysicalFileViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class FileStreamViewModelConverter : ITypeConverter<CatalogEntryStream, FileStreamViewModel>, ITypeConverter<FileStreamViewModel, CatalogEntryStream>
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IContentTypeProvider"/> provider.
		/// </summary>
		private IContentTypeProvider contentTypeProvider = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileStreamViewModelConverter"/> class.
		/// </summary>
		/// <param name="contentTypeProvider">The <see cref="IContentTypeProvider"/> provider.</param>
		public FileStreamViewModelConverter(IContentTypeProvider contentTypeProvider)
		{
			this.contentTypeProvider = contentTypeProvider;
		}

		#endregion

		#region ITypeConverter<CatalogEntryStream, FileStreamViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public FileStreamViewModel Convert(CatalogEntryStream source, FileStreamViewModel target)
		{
			target.ID = source.Entry.ID;
			target.FileName = source.Entry.Name;
			target.Size = source.Entry.Size.GetValueOrDefault();
			target.Path = source.Entry.Path;
			target.MimeType = this.contentTypeProvider?.GetContentType(source.Entry.Path);

			source.CopyTo(target.Stream);

			return target;
		}

		#endregion

		#region ITypeConverter<FileStreamViewModel, CatalogEntryStream> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntryStream Convert(FileStreamViewModel source, CatalogEntryStream target)
		{
			target.Entry.ID = source.ID;
			target.Entry.Name = source.FileName;
			target.Entry.Size = source.Size;
			target.Entry.Path = source.Path;

			return target;
		}

		#endregion
	}
}
