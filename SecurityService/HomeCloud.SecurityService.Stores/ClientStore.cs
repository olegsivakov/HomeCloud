namespace HomeCloud.IdentityService.Stores
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;

	using HomeCloud.Data.MongoDB;

	using IdentityServer4.Models;
	using IdentityServer4.Stores;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	#endregion

	/// <summary>
	/// Implements the retrieval of client configuration
	/// </summary>
	/// <seealso cref="IdentityServer4.Stores.IClientStore" />
	public class ClientStore : IClientStore
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
		/// Initializes a new instance of the <see cref="ClientStore" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public ClientStore(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IClientStore Implementations

		/// <summary>
		/// Finds a client by id
		/// </summary>
		/// <param name="clientId">The client id</param>
		/// <returns>
		/// The client
		/// </returns>
		public async Task<Client> FindClientByIdAsync(string clientId)
		{
			Guid id = Guid.Empty;

			if (Guid.TryParse(clientId, out id))
			{
				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
				ClientDocument document = await repository.GetAsync(id);
				if (document != null)
				{
					return this.mapper.MapNew<ClientDocument, Client>(document);
				}
			}

			return null;
		}

		#endregion
	}
}
