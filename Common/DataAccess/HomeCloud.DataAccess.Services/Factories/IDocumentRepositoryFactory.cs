namespace HomeCloud.DataAccess.Services.Factories
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines methods to create document repositories.
	/// </summary>
	public interface IDocumentRepositoryFactory : IRepositoryFactory
	{
		/// <summary>
		/// Gets the document-specific repository for specified data context.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IDocumentRepository"/>.</typeparam>
		/// <param name="context">The document context.</param>
		/// <returns> The instance of <see cref="IDocumentRepository" /> type.</returns>
		T GetRepository<T>(IDocumentContext context) where T : IDocumentRepository;
	}
}
