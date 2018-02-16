namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.Mvc.Providers;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="FileViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.FileViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.FileViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class FileViewModelConverter : ITypeConverter<CatalogEntry, FileViewModel>, ITypeConverter<FileViewModel, CatalogEntry>
	{
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
			target.ID = source.ID;
			target.FileName = source.Name;
			target.Size = source.Size.GetValueOrDefault();
			target.Path = source.Path;
			target.MimeType = this.contentTypeProvider?.GetContentType(source.Path);

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
			target.ID = source.ID;
			target.Name = source.FileName;
			target.Path = source.Path;
			target.Size = source.Size;
			target.Catalog = target.Catalog ?? new Catalog();

			return target;
		}

		#endregion
	}
}
