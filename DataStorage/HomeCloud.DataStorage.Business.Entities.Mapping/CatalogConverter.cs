namespace HomeCloud.DataStorage.Business.Entities.Mapping
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using Contracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Catalog" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.Mapping.TypeConverterBase" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Catalog, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Contracts.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument}" />
	public class CatalogConverter : TypeConverterBase, ITypeConverter<Contracts.Catalog, Catalog>, ITypeConverter<Catalog, Contracts.Catalog>, ITypeConverter<Contracts.CatalogDocument, Catalog>, ITypeConverter<Catalog, Contracts.CatalogDocument>
	{
		#region ITypeConverter<Contracts.Catalog, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(Contracts.Catalog source, Catalog target)
		{
			target = this.Validate(source, target);
			if (target == null)
			{
				return null;
			}

			target.ID = source.ID;
			target.ParentID = source.ParentID;
			target.Name = source.Name;
			target.StorageID = source.StorageID;
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<Catalog, Contracts.Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.Catalog Convert(Catalog source, Contracts.Catalog target)
		{
			target = this.Validate(source, target);
			if (target == null)
			{
				return null;
			}

			target.ID = source.ID;
			target.ParentID = source.ParentID;
			target.Name = source.Name;
			target.StorageID = source.StorageID;
			target.UpdatedDate = source.UpdatedDate;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<Contracts.CatalogDocument, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(Contracts.CatalogDocument source, Catalog target)
		{
			target = this.Validate(source, target);
			if (target == null)
			{
				return null;
			}

			if (target.ID == Guid.Empty)
			{
				target.ID = source.ID;
			}

			target.Path = source.Path;
			target.Size = source.Size;

			return target;
		}

		#endregion

		#region ITypeConverter<Catalog, Contracts.CatalogDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.CatalogDocument Convert(Catalog source, Contracts.CatalogDocument target)
		{
			target = this.Validate(source, target);
			if (target == null)
			{
				return null;
			}

			target.ID = source.ID;
			target.Path = source.Path;
			target.Size = source.Size;

			return target;
		}

		#endregion
	}
}
