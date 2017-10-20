namespace HomeCloud.DataStorage.DataAccess.Components.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Components.Factories;

	using HomeCloud.DataStorage.DataAccess.Components.Repositories;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	/// <summary>
	/// Provides the factory methods for repositories.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Components.Factories.DbQueryHandlerFactory" />
	public class DataStorageRepositoryFactory : DbQueryHandlerFactory
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataStorageRepositoryFactory"/> class.
		/// </summary>
		public DataStorageRepositoryFactory()
			: base(container =>
			{
				container.Add(typeof(IStorageRepository), typeof(StorageRepository));
				container.Add(typeof(IDirectoryRepository), typeof(DirectoryRepository));
				container.Add(typeof(IFileRepository), typeof(FileRepository));
			})
		{
		}

		#endregion
	}
}
