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
	public sealed class DocumentDataRepositoryFactory : DocumentRepositoryFactory
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentDataRepositoryFactory"/> class.
		/// </summary>
		public DocumentDataRepositoryFactory()
			: base(container =>
			{
				container.Add(typeof(ICatalogDocumentRepository), typeof(CatalogDocumentRepository));
				container.Add(typeof(IFileDocumentRepository), typeof(FileDocumentRepository));
			})
		{
		}

		#endregion
	}
}
