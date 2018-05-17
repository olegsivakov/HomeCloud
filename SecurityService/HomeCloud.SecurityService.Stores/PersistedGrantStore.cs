namespace HomeCloud.IdentityService.Stores
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using IdentityServer4.Models;
	using IdentityServer4.Stores;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	#endregion

	/// <summary>
	/// Provides methods to persist grants.
	/// </summary>
	/// <seealso cref="IdentityServer4.Stores.IPersistedGrantStore" />
	public class PersistedGrantStore : IPersistedGrantStore
	{
		#region Private Members

		/// <summary>
		/// The repository factory
		/// </summary>
		private readonly IServiceFactory<IMongoDBRepository> repositoryFactory = null;

		/// <summary>
		/// The mapper
		/// </summary>
		private readonly IMapper mapper = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PersistedGrantStore" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public PersistedGrantStore(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IPersistedGrantStore Implementations

		/// <summary>
		/// Gets all grants for a given subject id.
		/// </summary>
		/// <param name="subjectId">The subject identifier.</param>
		/// <returns></returns>
		public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
		{
			//identity.localhost/users/{id}/grants GET
			IGrantDocumentRepository repository = this.repositoryFactory.GetService<IGrantDocumentRepository>();
			IEnumerable<GrantDocument> documents = await repository.FindAllAsync(item => item.SubjectID == subjectId);

			return this.mapper.MapNew< GrantDocument, PersistedGrant>(documents);
		}

		/// <summary>
		/// Gets the grant.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public async Task<PersistedGrant> GetAsync(string key)
		{
			//identity.localhost/grants/{id} GET
			GrantDocument document = (await this.repositoryFactory.GetService<IGrantDocumentRepository>().FindAsync(item => item.ID == key)).FirstOrDefault();

			return this.mapper.MapNew<GrantDocument, PersistedGrant>(document);
		}

		/// <summary>
		/// Removes all grants for a given subject id and client id combination.
		/// </summary>
		/// <param name="subjectId">The subject identifier.</param>
		/// <param name="clientId">The client identifier.</param>
		/// <returns></returns>
		public async Task RemoveAllAsync(string subjectId, string clientId)
		{
			//identity.localhost/grants/clientId=<value>&userId=<value> DELETE
			await this.repositoryFactory.GetService<IGrantDocumentRepository>().DeleteAsync(item => item.SubjectID == subjectId && item.ClientID == clientId);
		}

		/// <summary>
		/// Removes all grants of a give type for a given subject id and client id combination.
		/// </summary>
		/// <param name="subjectId">The subject identifier.</param>
		/// <param name="clientId">The client identifier.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public async Task RemoveAllAsync(string subjectId, string clientId, string type)
		{
			//identity.localhost/grants/clientId=<value>&userId=<value>&type=<value> DELETE
			await this.repositoryFactory.GetService<IGrantDocumentRepository>().DeleteAsync(item => item.SubjectID == subjectId && item.ClientID == clientId && item.Type == type);
		}

		/// <summary>
		/// Removes the grant by key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public async Task RemoveAsync(string key)
		{
			//identity.localhost/grants/{id} DELETE
			await this.repositoryFactory.GetService<IGrantDocumentRepository>().DeleteAsync(item => item.ID == key);
		}

		/// <summary>
		/// Stores the grant.
		/// </summary>
		/// <param name="grant">The grant.</param>
		/// <returns></returns>
		public async Task StoreAsync(PersistedGrant grant)
		{
			//identity.localhost/grants/ POST
			GrantDocument document = this.mapper.MapNew<PersistedGrant, GrantDocument>(grant);

			await this.repositoryFactory.GetService<IGrantDocumentRepository>().SaveAsync(document);
		}

		#endregion
	}
}
