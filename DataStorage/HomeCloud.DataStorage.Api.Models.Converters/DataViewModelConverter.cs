namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using System.IO;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.Mvc.Providers;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="DataViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.DataViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.DataViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class DataViewModelConverter : ITypeConverter<CatalogEntry, DataViewModel>, ITypeConverter<DataViewModel, CatalogEntry>
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IContentTypeProvider"/> provider.
		/// </summary>
		private IContentTypeProvider contentTypeProvider = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataViewModelConverter" /> class.
		/// </summary>
		/// <param name="contentTypeProvider">The <see cref="IContentTypeProvider" /> provider.</param>
		public DataViewModelConverter(IContentTypeProvider contentTypeProvider)
		{
			this.contentTypeProvider = contentTypeProvider;
		}

		#endregion

		#region ITypeConverter<CatalogEntry, DataViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public DataViewModel Convert(CatalogEntry source, DataViewModel target)
		{
			target.ID = source.ID;
			target.Name = Path.GetFileNameWithoutExtension(source.Name);
			target.MimeType = this.contentTypeProvider?.GetContentType(source.Path);
			target.CreationDate = source.CreationDate;
			target.Size = source.Size.GetValueOrDefault();

			return target;
		}

		#endregion

		#region ITypeConverter<DataViewModel, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(DataViewModel source, CatalogEntry target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Size = source.Size;
			target.CreationDate = source.CreationDate;

			target.Catalog = target.Catalog ?? new Catalog();

			return target;
		}

		#endregion
	}
}
