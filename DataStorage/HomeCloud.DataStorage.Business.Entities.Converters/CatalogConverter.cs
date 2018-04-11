namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings
	
	using HomeCloud.Core;
	using HomeCloud.DataStorage.DataAccess.Aggregation.Objects;
	using System;
	using Contracts = HomeCloud.DataStorage.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Catalog" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Objects.Catalog, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Objects.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Aggregation.Objects.CatalogDocument}" />
	public class CatalogConverter : CatalogRootConverter, ITypeConverter<Contracts.Catalog, Catalog>, ITypeConverter<Catalog, Contracts.Catalog>, ITypeConverter<CatalogDocument, Catalog>, ITypeConverter<Catalog, CatalogDocument>
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
			this.Convert(source, (CatalogRoot)target);

			if (source.ParentID != target.Parent?.ID)
			{
				target.Parent = source.ParentID.HasValue ? new Catalog() { ID = source.ParentID.Value } : null;
			}

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
			target = this.Convert((CatalogRoot)source, target);

			Guid parentID = (source.Parent?.ID).GetValueOrDefault();
			if (parentID != Guid.Empty)
			{
				target.ParentID = source.Parent?.ID;
			}

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogDocument, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(CatalogDocument source, Catalog target)
		{
			this.Convert(source, (CatalogRoot)target);

			return target;
		}

		#endregion

		#region ITypeConverter<Catalog, CatalogDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogDocument Convert(Catalog source, CatalogDocument target)
		{
			target = this.Convert((CatalogRoot)source, target);

			return target;
		}

		#endregion
	}
}
