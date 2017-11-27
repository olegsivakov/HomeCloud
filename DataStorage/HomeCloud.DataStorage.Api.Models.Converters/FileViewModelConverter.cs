namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="FileViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Api.Models.Converters.DataViewModelConverter" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.FileViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.FileViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class FileViewModelConverter : DataViewModelConverter, ITypeConverter<CatalogEntry, FileViewModel>, ITypeConverter<FileViewModel, CatalogEntry>
	{
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

			target.Path = source.Path;

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

			return target;
		}

		#endregion
	}
}
