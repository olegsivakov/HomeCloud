namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using HomeCloud.Core;
	using Contracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Storage" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.DataAccess.Contracts.Storage, HomeCloud.DataStorage.Business.Entities.Storage}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.Storage, HomeCloud.DataStorage.DataAccess.Contracts.Storage}" />
	public class StorageConverter : ITypeConverter<Contracts.Storage, Storage>, ITypeConverter<Storage, Contracts.Storage>
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
			target.Name = source.Name;
			target.Quota = source.Quota;

			return target;
		}

		#endregion
	}
}
