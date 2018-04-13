namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;

	using Contracts = HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="CatalogRoot" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogRoot, HomeCloud.DataStorage.DataAccess.Objects.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Objects.Catalog, HomeCloud.DataStorage.Business.Entities.CatalogRoot}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogRoot, HomeCloud.DataStorage.Business.Entities.CatalogRoot}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogRoot, CatalogContract}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{CatalogContract, HomeCloud.DataStorage.Business.Entities.CatalogRoot}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogRoot, HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument, HomeCloud.DataStorage.Business.Entities.CatalogRoot}" />
	public class CatalogRootConverter : ITypeConverter<CatalogRoot, Contracts.Catalog>, ITypeConverter<Contracts.Catalog, CatalogRoot>, ITypeConverter<CatalogRoot, CatalogDocument>, ITypeConverter<CatalogDocument, CatalogRoot>, ITypeConverter<CatalogRoot, CatalogRoot>
	{
		#region ITypeConverter<CatalogRoot, Contracts.Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="CatalogRoot" /> type to the instance of <see cref="CatalogContract" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="CatalogRoot" />.</param>
		/// <param name="target">The instance of <see cref="Contracts.Catalog" />.</param>
		/// <returns>
		/// The converted instance of <see cref="Contracts.Catalog" />.
		/// </returns>
		public Contracts.Catalog Convert(CatalogRoot source, Contracts.Catalog target)
		{
			target.ID = source.ID;
			target.Name = string.IsNullOrWhiteSpace(source.Name) ? target.Name : source.Name.Trim();
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<Contracts.Catalog, CatalogRoot> Implementations

		/// <summary>
		/// Converts the instance of <see cref="Contracts.Catalog" /> type to the instance of <see cref="CatalogRoot" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="Contracts.Catalog" />.</param>
		/// <param name="target">The instance of <see cref="CatalogRoot" />.</param>
		/// <returns>
		/// The converted instance of <see cref="CatalogRoot" />.
		/// </returns>
		public CatalogRoot Convert(Contracts.Catalog source, CatalogRoot target)
		{
			if (target.ID == Guid.Empty)
			{
				target.ID = source.ID;
			}

			target.Name = source.Name;
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

		#region ITypeConverter<CatalogRoot, CatalogRoot> Implementations

		/// <summary>
		/// Converts the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		public CatalogRoot Convert(CatalogRoot source, CatalogRoot target)
		{
			target.ID = target.ID == Guid.Empty ? source.ID : target.ID;
			target.Name = string.IsNullOrWhiteSpace(target.Name) ? source.Name : target.Name;
			target.Path = string.IsNullOrWhiteSpace(target.Path) ? source.Path : target.Path;
			target.Size = !target.Size.HasValue ? source.Size : target.Size;
			target.CreationDate = target.CreationDate == DateTime.MinValue ? source.CreationDate : target.CreationDate;
			target.UpdatedDate = target.UpdatedDate == DateTime.MinValue ? source.UpdatedDate : target.UpdatedDate;

			return target;
		}

		#endregion
	}
}
