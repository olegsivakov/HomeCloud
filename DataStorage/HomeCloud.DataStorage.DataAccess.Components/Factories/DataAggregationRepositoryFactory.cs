namespace HomeCloud.DataStorage.DataAccess.Components.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Components.Factories;

	using HomeCloud.DataStorage.DataAccess.Components.Repositories;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	/// <summary>
	/// Provides the factory methods for document repositories.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Components.Factories.DocumentRepositoryFactory" />
	public sealed class DataAggregationRepositoryFactory : DocumentRepositoryFactory
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataAggregationRepositoryFactory"/> class.
		/// </summary>
		public DataAggregationRepositoryFactory()
			: base(container =>
			{
				container.Add(typeof(ICatalogAggregationRepository), typeof(CatalogAggregationRepository));
				container.Add(typeof(IFileAggregationRepository), typeof(FileAggregationRepository));
			})
		{
		}

		#endregion
	}
}
