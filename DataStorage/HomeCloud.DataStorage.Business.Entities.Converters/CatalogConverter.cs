namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using Contracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Catalog" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Catalog, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Contracts.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument}" />
	public class CatalogConverter : CatalogRootConverter, ITypeConverter<Contracts.Catalog, Catalog>, ITypeConverter<Catalog, Contracts.Catalog>, ITypeConverter<Contracts.CatalogDocument, Catalog>, ITypeConverter<Catalog, Contracts.CatalogDocument>
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

			target.ParentID = source.Parent?.ID;

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
			this.Convert(source, (CatalogRoot)target);

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
			target = this.Convert((CatalogRoot)source, target);

			return target;
		}

		#endregion
	}
}
