

namespace HomeCloud.IdentityService.Stores
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using IdentityServer4.Models;
	using IdentityServer4.Stores;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;
	using System.Linq;

	#endregion

	/// <summary>
	/// Provides methods for resource retrieval.
	/// </summary>
	/// <seealso cref="IdentityServer4.Stores.IResourceStore" />
	public class ResourceStore : IResourceStore
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
		/// Initializes a new instance of the <see cref="ResourceStore" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public ResourceStore(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IResourceStore Implementations

		/// <summary>
		/// Finds the API resource by name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public async Task<ApiResource> FindApiResourceAsync(string name)
		{
			Guid id = Guid.Empty;

			if (Guid.TryParse(name, out id))
			{
				IResourceDocumentRepository repository = this.repositoryFactory.GetService<IResourceDocumentRepository>();
				ResourceDocument document = await repository.GetAsync(id);
				if (document != null)
				{
					return this.mapper.MapNew<ResourceDocument, ApiResource>(document);
				}
			}

			return null;
		}

		/// <summary>
		/// Gets API resources by scope name.
		/// </summary>
		/// <param name="scopeNames"></param>
		/// <returns></returns>
		public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			if (!(scopeNames?.Any()).GetValueOrDefault())
			{
				return Enumerable.Empty<ApiResource>();
			}

			int offset = 0;
			int limit = 20;
			int count = 0;

			List<ResourceDocument> result = new List<ResourceDocument>();
			IResourceDocumentRepository repository = this.repositoryFactory.GetService<IResourceDocumentRepository>();

			do
			{
				IPaginable<ResourceDocument> documents = await repository.FindAsync(item => item.Scopes != null && item.Scopes.Any(scope => scopeNames.Any(name => name == scope)), offset, limit);
				result.AddRange(documents);

				offset += documents.Offset + limit;
				count = documents.TotalCount;
			}
			while(count >= offset);

			return this.mapper.MapNew<ResourceDocument, ApiResource>(result);
		}

		public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			throw new System.NotImplementedException();
		}

		public async Task<Resources> GetAllResourcesAsync()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
