namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="StorageViewModel" /> entity.
	/// </summary>
	public class StorageViewModelConverter : ITypeConverter<Storage, StorageViewModel>, ITypeConverter<StorageViewModel, Storage>
	{
		#region  ITypeConverter<Storage, StorageViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public StorageViewModel Convert(Storage source, StorageViewModel target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Quota = source.Quota;
			target.Size = source.CatalogRoot.Size;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion

		#region ITypeConverter<StorageViewModel, Storage> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Storage Convert(StorageViewModel source, Storage target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.Quota = source.Quota;
			target.CreationDate = source.CreationDate;

			return target;
		}

		#endregion
	}
}
