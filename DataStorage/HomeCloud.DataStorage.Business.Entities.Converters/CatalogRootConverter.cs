namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using CatalogContract = HomeCloud.DataStorage.DataAccess.Contracts.Catalog;
	using CatalogDocument = HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="CatalogRoot" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogRoot, HomeCloud.DataStorage.DataAccess.Contracts.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Catalog, HomeCloud.DataStorage.Business.Entities.CatalogRoot}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogRoot, HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument, HomeCloud.DataStorage.Business.Entities.CatalogRoot}" />
	public class CatalogRootConverter : ITypeConverter<CatalogRoot, CatalogContract>, ITypeConverter<CatalogContract, CatalogRoot>, ITypeConverter<CatalogRoot, CatalogDocument>, ITypeConverter<CatalogDocument, CatalogRoot>
	{
		#region ITypeConverter<CatalogRoot, CatalogContract> Implementations

		/// <summary>
		/// Converts the instance of <see cref="CatalogRoot" /> type to the instance of <see cref="CatalogContract" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="CatalogRoot" />.</param>
		/// <param name="target">The instance of <see cref="CatalogContract" />.</param>
		/// <returns>
		/// The converted instance of <see cref="CatalogContract" />.
		/// </returns>
		public CatalogContract Convert(CatalogRoot source, CatalogContract target)
		{
			target.ID = source.ID;
			target.Name = string.IsNullOrWhiteSpace(source.Name) ? target.Name : source.Name.Trim();
			target.StorageID = source.StorageID == Guid.Empty ? target.StorageID : source.StorageID;
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogContract, CatalogRoot> Implementations

		/// <summary>
		/// Converts the instance of <see cref="CatalogContract" /> type to the instance of <see cref="CatalogRoot" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="CatalogContract" />.</param>
		/// <param name="target">The instance of <see cref="CatalogRoot" />.</param>
		/// <returns>
		/// The converted instance of <see cref="CatalogRoot" />.
		/// </returns>
		public CatalogRoot Convert(CatalogContract source, CatalogRoot target)
		{
			if (target.ID == Guid.Empty)
			{
				target.ID = source.ID;
			}

			target.Name = source.Name;
			target.StorageID = source.StorageID;
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogRoot, CatalogDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="CatalogRoot" /> type to the instance of <see cref="CatalogDocument" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="CatalogRoot" />.</param>
		/// <param name="target">The instance of <see cref="CatalogDocument" />.</param>
		/// <returns>
		/// The converted instance of <see cref="CatalogDocument" />.
		/// </returns>
		public CatalogDocument Convert(CatalogRoot source, CatalogDocument target)
		{
			target.ID = source.ID;
			target.Path = string.IsNullOrWhiteSpace(source.Path) ? target.Path : source.Path;
			target.Size = source.Size.HasValue ? source.Size.Value : target.Size;

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogDocument, CatalogRoot> Implementations

		/// <summary>
		/// Converts the instance of <see cref="CatalogDocument" /> type to the instance of <see cref="CatalogRoot" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="CatalogDocument" />.</param>
		/// <param name="target">The instance of <see cref="CatalogRoot" />.</param>
		/// <returns>
		/// The converted instance of <see cref="CatalogRoot" />.
		/// </returns>
		public CatalogRoot Convert(CatalogDocument source, CatalogRoot target)
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
