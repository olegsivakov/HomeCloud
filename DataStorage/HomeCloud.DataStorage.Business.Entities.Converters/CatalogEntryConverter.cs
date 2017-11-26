namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using File = HomeCloud.DataStorage.DataAccess.Contracts.File;
	using FileDocument = HomeCloud.DataStorage.DataAccess.Contracts.FileDocument;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="CatalogEntry" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.DataAccess.Contracts.File}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.File, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.DataAccess.Contracts.FileDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.FileDocument, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class CatalogEntryConverter : ITypeConverter<CatalogEntry, File>, ITypeConverter<File, CatalogEntry>, ITypeConverter<CatalogEntry, FileDocument>, ITypeConverter<FileDocument, CatalogEntry>
	{
		#region ITypeConverter<CatalogEntry, File> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public File Convert(CatalogEntry source, File target)
		{
			target.ID = source.ID;
			target.Name = string.IsNullOrWhiteSpace(source.Name) ? target.Name : source.Name.Trim();
			target.Extension = string.IsNullOrWhiteSpace(source.Extension) ? target.Extension : source.Extension.Trim();
			target.DirectoryID = (source.Catalog?.ID).GetValueOrDefault();

			return target;
		}

		#endregion

		#region ITypeConverter<File, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(File source, CatalogEntry target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Extension = source.Extension;
			target.Catalog = new Catalog() { ID = source.ID };
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogEntry, FileDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public FileDocument Convert(CatalogEntry source, FileDocument target)
		{
			target.ID = source.ID;
			target.Path = string.IsNullOrWhiteSpace(source.Path) ? target.Path : source.Path;
			target.Size = source.Size.HasValue ? source.Size.Value : target.Size;

			return target;
		}

		#endregion

		#region ITypeConverter<FileDocument, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(FileDocument source, CatalogEntry target)
		{
			if (target.ID == Guid.Empty)
			{
				target.ID = source.ID;
			}

			target.Path = source.Path;
			target.Size = source.Size;

			return target;
		}

		#endregion
	}
}
