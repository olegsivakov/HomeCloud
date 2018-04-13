﻿namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.Mvc.Providers;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="FileStreamViewModel" /> entity.
	/// </summary>
	public class FileStreamViewModelConverter : ITypeConverter<CatalogEntry, FileStreamViewModel>, ITypeConverter<FileStreamViewModel, CatalogEntry>
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
		public FileStreamViewModelConverter(IContentTypeProvider contentTypeProvider)
		{
			this.contentTypeProvider = contentTypeProvider;
		}

		#endregion

		#region ITypeConverter<CatalogEntry, FileStreamViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public FileStreamViewModel Convert(CatalogEntry source, FileStreamViewModel target)
		{
			target.FileName = source.Name;
			target.Path = source.Path;
			target.MimeType = !string.IsNullOrWhiteSpace(source.Path) ? this.contentTypeProvider?.GetContentType(source.Path) : null;
			target.Size = source.Size.GetValueOrDefault();

			return target;
		}

		#endregion

		#region ITypeConverter<FileStreamViewModel, CatalogEntry> Implementations

		public CatalogEntry Convert(FileStreamViewModel source, CatalogEntry target)
		{
			target.Name = source.FileName;
			target.Size = source.Size;
			target.Catalog = new Catalog();

			return target;
		}

		#endregion
	}
}