namespace HomeCloud.DataAccess.Components
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	/// Provides methods for operations within data context executed as single scope.
	/// </summary>
	/// <seealso cref="IDocumentContextScope" />
	public class DocumentContextScope : IDocumentContextScope
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IDocumentRepositoryFactory"/> member.
		/// </summary>
		private static IDocumentRepositoryFactory repositoryFactory = null;

		/// <summary>
		/// The <see cref="IDocumentContext"/> member.
		/// </summary>
		private readonly IDocumentContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentContextScope" /> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="repositoryFactory">The <see cref="IDocumentRepositoryFactory" /> factory.</param>
		public DocumentContextScope(string connectionString, IDocumentRepositoryFactory repositoryFactory = null)
		{
			DocumentContextScope.repositoryFactory = repositoryFactory;

			this.context = new DocumentContext(connectionString);
		}

		#endregion

		#region IDocumentContextScope Implementations

		/// <summary>
		/// Gets the <see cref="T:HomeCloud.DataAccess.Services.IDocumentRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="T:HomeCloud.DataAccess.Services.IDocumentRepository" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.IDocumentRepository" />.
		/// </returns>
		public T GetRepository<T>() where T : IDocumentRepository
		{
			return repositoryFactory.GetRepository<T>(this.context);
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
		}

		#endregion
	}
}
