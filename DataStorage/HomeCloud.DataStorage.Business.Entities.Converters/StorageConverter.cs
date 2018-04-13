﻿namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;

	using Contracts = HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Storage" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.Converters.CatalogRootConverter" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Objects.Catalog, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Objects.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Objects.Storage, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Objects.Storage}" />
	public class StorageConverter
		: CatalogRootConverter,
		ITypeConverter<Contracts.Storage, Storage>, ITypeConverter<Storage, Contracts.Storage>,
		ITypeConverter<Contracts.Catalog, Storage>, ITypeConverter<Storage, Contracts.Catalog>,
		ITypeConverter<CatalogDocument, Storage>, ITypeConverter<Storage, CatalogDocument>,
		ITypeConverter<Storage, Storage>
	{
		#region ITypeConverter<Contracts.Storage, Storage> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Storage Convert(Contracts.Storage source, Storage target)
		{
			target.ID = source.ID;
			target.DisplayName = source.Name;
			target.Quota = source.Quota;
			target.CreationDate = source.CreationDate;
			target.UpdatedDate = source.UpdatedDate;

			return target;
		}

		#endregion

		#region ITypeConverter<Storage, Contracts.Storage> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.Storage Convert(Storage source, Contracts.Storage target)
		{
			target.ID = source.ID;
			target.Name = string.IsNullOrWhiteSpace(source.DisplayName) ? target.Name : source.DisplayName.Trim();
			target.Quota = source.Quota.HasValue ? source.Quota.Value : target.Quota;

			return target;
		}

		#endregion

		#region ITypeConverter<Contracts.Catalog, Storage> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Storage Convert(Contracts.Catalog source, Storage target)
		{
			this.Convert(source, (CatalogRoot)target);

			return target;
		}

		#endregion

		#region ITypeConverter<Storage, Contracts.Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.Catalog Convert(Storage source, Contracts.Catalog target)
		{
			return this.Convert((CatalogRoot)source, target);
		}

		#endregion

		#region ITypeConverter<Contracts.Catalog, Storage> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Storage Convert(CatalogDocument source, Storage target)
		{
			this.Convert(source, (CatalogRoot)target);

			return target;
		}

		#endregion

		#region ITypeConverter<Storage, CatalogDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogDocument Convert(Storage source, CatalogDocument target)
		{
			return this.Convert((CatalogRoot)source, target);
		}

		#endregion

		#region ITypeConverter<Storage, Storage> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Storage Convert(Storage source, Storage target)
		{
			target = (Storage)this.Convert((CatalogRoot)source, target);

			target.Quota = !target.Quota.HasValue ? source.Quota : target.Quota;
			target.DisplayName = string.IsNullOrWhiteSpace(target.DisplayName) ? source.DisplayName : target.DisplayName;

			return target;
		}

		#endregion
	}
}
