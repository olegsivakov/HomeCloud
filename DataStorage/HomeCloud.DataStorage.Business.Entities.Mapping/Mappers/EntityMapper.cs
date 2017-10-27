namespace HomeCloud.DataStorage.Business.Entities.Mapping.Mappers
{
	#region Usings

	using HomeCloud.Core;

	using Contracts = HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Provides mapping for <see cref="HomeCloud.DataStorage.Business.Entities"/> entities.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.Mapper" />
	public class EntityMapper : Mapper
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="EntityMapper"/> class.
		/// </summary>
		/// <param name="storageConverter">The storage converter.</param>
		/// <param name="storageContractConverter">The storage contract converter.</param>
		public EntityMapper(
			ITypeConverter<Contracts.Storage, Storage> storageConverter,
			ITypeConverter<Storage, Contracts.Storage> storageContractConverter)
			: base(
				container =>
				{
					container.Add(typeof(ITypeConverter<Contracts.Storage, Storage>), storageConverter);
					container.Add(typeof(ITypeConverter<Storage, Contracts.Storage>), storageContractConverter);
				})
		{
		}

		#endregion
	}
}
