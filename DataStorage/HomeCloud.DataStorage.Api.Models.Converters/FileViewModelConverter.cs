namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using System.IO;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.Mvc.Providers;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="FileViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.FileViewModel}" />
	public class FileViewModelConverter : ITypeConverter<CatalogEntry, FileViewModel>
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
			target.Name = source.Name;
			target.Size = new SizeViewModel(source.Size.GetValueOrDefault());
			target.CreationDate = source.CreationDate;
			target.Type = string.IsNullOrWhiteSpace(source.Path) ? string.Empty : Path.GetExtension(source.Path)?.Replace(".", string.Empty);

			return target;
		}

		#endregion
	}
}
