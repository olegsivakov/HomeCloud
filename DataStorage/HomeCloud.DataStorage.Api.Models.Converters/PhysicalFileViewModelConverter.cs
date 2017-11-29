namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Api.Providers;
	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="PhysicalFileViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.PhysicalFileViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.PhysicalFileViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class PhysicalFileViewModelConverter : ITypeConverter<CatalogEntry, PhysicalFileViewModel>, ITypeConverter<PhysicalFileViewModel, CatalogEntry>
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IContentTypeProvider"/> provider.
		/// </summary>
		private IContentTypeProvider contentTypeProvider = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PhysicalFileViewModelConverter"/> class.
		/// </summary>
		/// <param name="contentTypeProvider">The <see cref="IContentTypeProvider"/> provider.</param>
		public PhysicalFileViewModelConverter(IContentTypeProvider contentTypeProvider)
		{
			this.contentTypeProvider = contentTypeProvider;
		}

		#endregion

		#region ITypeConverter<CatalogEntry, PhysicalFileViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public PhysicalFileViewModel Convert(CatalogEntry source, PhysicalFileViewModel target)
		{
			target.ID = source.ID;
			target.FileName = source.Name;
			target.Size = source.Size.GetValueOrDefault();
			target.Path = source.Path;
			target.MimeType = this.contentTypeProvider?.GetContentType(source.Path);

			return target;
		}

		#endregion

		#region ITypeConverter<PhysicalFileViewModel, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(PhysicalFileViewModel source, CatalogEntry target)
		{
			target.ID = source.ID;
			target.Name = source.FileName;
			target.Path = source.Path;

			return target;
		}

		#endregion
	}
}
