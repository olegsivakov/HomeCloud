namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;
	using HomeCloud.DataStorage.DataAccess.Objects;

	using Catalog = HomeCloud.DataStorage.Business.Entities.Catalog;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="CatalogEntry" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.DataAccess.Objects.File}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Objects.File, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.DataAccess.Aggregation.Objects.FileDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Aggregation.Objects.FileDocument, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	public class CatalogEntryConverter : ITypeConverter<CatalogEntry, File>, ITypeConverter<File, CatalogEntry>, ITypeConverter<CatalogEntry, FileDocument>, ITypeConverter<FileDocument, CatalogEntry>, ITypeConverter<CatalogEntry, CatalogEntry>
	{
		#region Private Members

		/// <summary>
		/// The catalog converter
		/// </summary>
		private readonly ITypeConverter<Catalog, Catalog> catalogConverter = null;
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogEntryConverter"/> class.
		/// </summary>
		/// <param name="catalogConverter">The catalog converter.</param>
		public CatalogEntryConverter(ITypeConverter<Catalog, Catalog> catalogConverter)
		{
			this.catalogConverter = catalogConverter;
		}

		#endregion

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
			target.Catalog = new Catalog() { ID = source.DirectoryID };
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
		#region ITypeConverter<CatalogEntry, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(CatalogEntry source, CatalogEntry target)
		{
			target.ID = target.ID == Guid.Empty ? source.ID : target.ID;
			target.Name = string.IsNullOrWhiteSpace(target.Name) ? source.Name : target.Name;
			target.Path = string.IsNullOrWhiteSpace(target.Path) ? source.Path : target.Path;
			target.Size = !target.Size.HasValue ? source.Size : target.Size;
			target.CreationDate = target.CreationDate == DateTime.MinValue ? source.CreationDate : target.CreationDate;
			target.UpdatedDate = target.UpdatedDate == DateTime.MinValue ? source.UpdatedDate : target.UpdatedDate;
			target.Catalog = this.catalogConverter.Convert(source.Catalog, target.Catalog);

			return target;
		}

		#endregion

	}
}
