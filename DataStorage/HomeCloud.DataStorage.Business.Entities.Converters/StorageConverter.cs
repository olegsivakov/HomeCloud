namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using HomeCloud.Core;

	using Contracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Storage" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Entities.Converters.CatalogRootConverter" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Catalog, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Contracts.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Contracts.CatalogDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Storage, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Contracts.Storage}" />
	public class StorageConverter : CatalogRootConverter, ITypeConverter<Contracts.Storage, Storage>, ITypeConverter<Storage, Contracts.Storage>, ITypeConverter<Contracts.Catalog, Storage>, ITypeConverter<Storage, Contracts.Catalog>, ITypeConverter<Contracts.CatalogDocument, Storage>, ITypeConverter<Storage, Contracts.CatalogDocument>
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
			target.Name = source.Name;
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
			target.Name = string.IsNullOrWhiteSpace(source.Name) ? target.Name : source.Name.Trim();
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
		public Contracts.Catalog Convert(Storage target, Contracts.Catalog source)
		{
			return this.Convert((CatalogRoot)target, source);
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
		public Storage Convert(Contracts.CatalogDocument source, Storage target)
		{
			this.Convert(source, (CatalogRoot)target);

			return target;
		}

		#endregion

		#region ITypeConverter<Storage, Contracts.CatalogDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Contracts.CatalogDocument Convert(Storage target, Contracts.CatalogDocument source)
		{
			return this.Convert((CatalogRoot)target, source);
		}

		#endregion
	}
}
