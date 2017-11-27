﻿namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="DataViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.DataViewModel, HomeCloud.DataStorage.Business.Entities.Catalog}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Api.Models.DataViewModel, HomeCloud.DataStorage.Business.Entities.CatalogEntry}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Catalog, HomeCloud.DataStorage.Api.Models.DataViewModel}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.CatalogEntry, HomeCloud.DataStorage.Api.Models.DataViewModel}" />
	public class DataViewModelConverter : ITypeConverter<Catalog, DataViewModel>, ITypeConverter<DataViewModel, Catalog>, ITypeConverter<CatalogEntry, DataViewModel>, ITypeConverter<DataViewModel, CatalogEntry>
	{
		#region ITypeConverter<Catalog, DataViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public DataViewModel Convert(Catalog source, DataViewModel target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.IsCatalog = true;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<DataViewModel, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(DataViewModel source, Catalog target)
		{
			target.ID = source.ID;
			target.Parent = new Catalog();
			target.Name = source.Name;

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogEntry, DataViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public DataViewModel Convert(CatalogEntry source, DataViewModel target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.IsCatalog = false;
			target.CreationDate = source.CreationDate;
			target.Size = source.Size.GetValueOrDefault();

			return target;
		}

		#endregion

		#region ITypeConverter<DataViewModel, CatalogEntry> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogEntry Convert(DataViewModel source, CatalogEntry target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Catalog = new Catalog();

			return target;
		}

		#endregion
	}
}
