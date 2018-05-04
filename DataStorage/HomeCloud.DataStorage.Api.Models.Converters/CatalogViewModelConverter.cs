namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="CatalogViewModel" /> entity.
	/// </summary>
	public class CatalogViewModelConverter : ITypeConverter<Catalog, CatalogViewModel>, ITypeConverter<CatalogViewModel, Catalog>
	{
		#region  ITypeConverter<Catalog, CatalogViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public CatalogViewModel Convert(Catalog source, CatalogViewModel target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Exists = source.Exists;
			target.CreationDate = source.CreationDate;
			target.Size = new SizeViewModel(source.Size.GetValueOrDefault());

			return target;
		}

		#endregion

		#region ITypeConverter<CatalogViewModel, Catalog> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Catalog Convert(CatalogViewModel source, Catalog target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Exists = source.Exists;
			target.CreationDate = source.CreationDate;
			target.Parent = target.Parent ?? new Catalog();

			target.Size = source.Size?.Value;

			return target;
		}

		#endregion
	}
}
