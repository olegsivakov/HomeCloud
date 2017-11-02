namespace HomeCloud.DataStorage.Business.Entities.Mapping
{
	#region Usings

	using HomeCloud.Core;
	using System;
	using Contracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Catalog" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.Mapping.TypeConverterBase" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Directory, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Contracts.Directory}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.AggregatedCatalog, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Contracts.AggregatedCatalog}" />
	public class CatalogConverter : TypeConverterBase, ITypeConverter<Contracts.Directory, Catalog>, ITypeConverter<Catalog, Contracts.Directory>, ITypeConverter<Contracts.AggregatedCatalog, Catalog>, ITypeConverter<Catalog, Contracts.AggregatedCatalog>
	{
		#region ITypeConverter<Contracts.Directory, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(Contracts.Directory source, Catalog target)
		{
			target = this.Validate(source, target);

			target.ID = source.ID;
			target.ParentID = source.ParentID;
			target.Name = source.Name;
			target.StorageID = source.StorageID;
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<Catalog, Contracts.Directory> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.Directory Convert(Catalog source, Contracts.Directory target)
		{
			target = this.Validate(source, target);

			target.ID = source.ID;
			target.ParentID = source.ParentID;
			target.Name = source.Name;
			target.StorageID = source.StorageID;
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<Contracts.AggregatedCatalog, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(Contracts.AggregatedCatalog source, Catalog target)
		{
			target = this.Validate(source, target);

			if (target.ID == Guid.Empty)
			{
				target.ID = source.ID;
			}

			target.Path = source.Path;
			target.Size = source.Size;

			return target;
		}

		#endregion

		#region ITypeConverter<Catalog, Contracts.AggregatedCatalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.AggregatedCatalog Convert(Catalog source, Contracts.AggregatedCatalog target)
		{
			target = this.Validate(source, target);

			target.ID = source.ID;
			target.Path = source.Path;
			target.Size = source.Size;

			return target;
		}

		#endregion
	}
}
