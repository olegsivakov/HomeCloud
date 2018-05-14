

namespace HomeCloud.IdentityService.Stores
{
	#region Usings

	using System;
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
				IApiResourceDocumentRepository repository = this.repositoryFactory.GetService<IApiResourceDocumentRepository>();
				ApiResourceDocument document = await repository.GetAsync(id);
				if (document != null)
				{
					return this.mapper.MapNew<ApiResourceDocument, ApiResource>(document);
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

			IApiResourceDocumentRepository repository = this.repositoryFactory.GetService<IApiResourceDocumentRepository>();
			IEnumerable<ApiResourceDocument> documents = await repository.FindAllAsync(item => scopeNames.Any(name => item.Name.ToLower() == name.ToLower()));

			return this.mapper.MapNew<ApiResourceDocument, ApiResource>(documents);
		}

		/// <summary>
		/// Gets identity resources by scope name.
		/// </summary>
		/// <param name="scopeNames"></param>
		/// <returns></returns>
		public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			if (!(scopeNames?.Any()).GetValueOrDefault())
			{
				return Enumerable.Empty<IdentityResource>();
			}

			IIdentityResourceDocumentRepository repository = this.repositoryFactory.GetService<IIdentityResourceDocumentRepository>();
			IEnumerable<IdentityResourceDocument> documents = await repository.FindAllAsync(item => scopeNames.Any(name => item.Name.ToLower() == name.ToLower()));

			return this.mapper.MapNew<IdentityResourceDocument, IdentityResource>(documents);
		}

		/// <summary>
		/// Gets all resources.
		/// </summary>
		/// <returns></returns>
		public async Task<Resources> GetAllResourcesAsync()
		{
			IEnumerable<ApiResourceDocument> apiResourceDocuments = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().FindAllAsync(null);
			IEnumerable<IdentityResourceDocument> identityResourceDocuments = await this.repositoryFactory.GetService<IIdentityResourceDocumentRepository>().FindAllAsync(null);

			IEnumerable<ApiResource> apiResources = this.mapper.MapNew<ApiResourceDocument, ApiResource>(apiResourceDocuments);
			IEnumerable<IdentityResource> identityResources = this.mapper.MapNew<IdentityResourceDocument, IdentityResource>(identityResourceDocuments);

			return new Resources(identityResources, apiResources);
		}

		#endregion
	}
}
