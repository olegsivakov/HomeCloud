namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Transactions;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Validation;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to manage <see cref="Api"/> resource applications.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Services.IResourceService" />
	public class ResourceService : IResourceService
	{
		#region Private Members

		/// <summary>
		/// The validation service factory
		/// </summary>
		private readonly IValidationServiceFactory validationServiceFactory = null;

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
		/// Initializes a new instance of the <see cref="ResourceService" /> class.
		/// </summary>
		/// <param name="validationServiceFactory">The validation service factory.</param>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public ResourceService(
			IValidationServiceFactory validationServiceFactory,
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.validationServiceFactory = validationServiceFactory;
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IResourceService Implementations

		/// <summary>
		/// Creates new api resource <paramref name="application" />.
		/// </summary>
		/// <param name="application">The api resource application.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<ApiResource>> CreateApplicationAsync(ApiResource application)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				application.ID = Guid.Empty;

				IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();

				ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(application);
				result += await validator.Get<IUniqueValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<ApiResource>(application)
					{
						Errors = result.Errors
					};
				}

				ApiResourceDocument document = this.mapper.MapNew<ApiResource, ApiResourceDocument>(application);
				document = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().SaveAsync(document);
				application = this.mapper.Map(document, application);

				scope.Complete();
			}

			return new ServiceResult<ApiResource>(application);
		}

		/// <summary>
		/// Searches for the list of api resource applications by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The search criteria.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IPaginable<ApiResource>>> FindApplicationsAsync(ApiResource criteria, int offset = 0, int limit = 20)
		{
			IPaginable<ApiResourceDocument> documents = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().FindAsync(document =>
				criteria == null
				||
				(
					(string.IsNullOrWhiteSpace(criteria.Name) || document.Name.Trim().ToLower().Contains(criteria.Name.Trim().ToLower()))
				), offset, limit);

			IEnumerable<ApiResource> result = this.mapper.MapNew<ApiResourceDocument, ApiResource>(documents);

			return new ServiceResult<IPaginable<ApiResource>>(new PagedList<ApiResource>(result)
			{
				Offset = documents.Offset,
				Limit = documents.Limit,
				TotalCount = documents.TotalCount
			});
		}

		/// <summary>
		/// Gets api resource application by specified api resource application identifier.
		/// </summary>
		/// <param name="id">The api resource identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<ApiResource>> GetApplicationAsync(Guid id)
		{
			ApiResource application = new ApiResource()
			{
				ID = id
			};

			IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<ApiResource>(application)
				{
					Errors = result.Errors
				};
			}

			ApiResourceDocument document = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().GetAsync(application.ID);
			application = this.mapper.Map(document, application);

			return new ServiceResult<ApiResource>(application);
		}

		/// <summary>
		/// Gets the list of application claims by specified api resource application identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> GetClaimsAsync(Guid applicationID)
		{
			ApiResource application = new ApiResource()
			{
				ID = applicationID
			};

			IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<string> documents = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().FindClaims(resource => resource.ID == application.ID, null);

			return new ServiceResult<IEnumerable<string>>(documents);
		}

		/// <summary>
		/// Gets the list of application scopes by specified api resource application identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> GetScopesAsync(Guid applicationID)
		{
			ApiResource application = new ApiResource()
			{
				ID = applicationID
			};

			IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<string> documents = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().FindScopes(resource => resource.ID == application.ID, null);

			return new ServiceResult<IEnumerable<string>>(documents);
		}

		/// <summary>
		/// Gets the list of application secrets by specified api resource application identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Secret>>> GetSecretsAsync(Guid applicationID)
		{
			ApiResource application = new ApiResource()
			{
				ID = applicationID
			};

			IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<Secret>>(Enumerable.Empty<Secret>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<SecretDocument> documents = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().FindSecrets(resource => resource.ID == application.ID, null);
			IEnumerable<Secret> secrets = this.mapper.MapNew<SecretDocument, Secret>(documents);

			return new ServiceResult<IEnumerable<Secret>>(secrets);
		}

		/// <summary>
		/// Saves the list of application clames for the api resource application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <param name="claims">The list of claims.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> SaveClaimsAsync(Guid applicationID, IEnumerable<string> claims)
		{
			ApiResource application = new ApiResource()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
					{
						Errors = result.Errors
					};
				}

				IApiResourceDocumentRepository repository = this.repositoryFactory.GetService<IApiResourceDocumentRepository>();

				ApiResourceDocument document = this.mapper.MapNew<ApiResource, ApiResourceDocument>(application);
				document.Claims = claims ?? Enumerable.Empty<string>();

				document = await repository.SaveClaims(document);

				scope.Complete();

				return new ServiceResult<IEnumerable<string>>(document.Claims);
			}
		}

		/// <summary>
		/// Saves the list of application scopes for the api resource application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <param name="scopes">The list of scopes.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> SaveScopesAsync(Guid applicationID, IEnumerable<string> scopes)
		{
			ApiResource application = new ApiResource()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
					{
						Errors = result.Errors
					};
				}

				IApiResourceDocumentRepository repository = this.repositoryFactory.GetService<IApiResourceDocumentRepository>();

				ApiResourceDocument document = this.mapper.MapNew<ApiResource, ApiResourceDocument>(application);
				document.Scopes = scopes ?? Enumerable.Empty<string>();

				document = await repository.SaveScopes(document);

				scope.Complete();

				return new ServiceResult<IEnumerable<string>>(document.Scopes);
			}
		}

		/// <summary>
		/// Saves the list of application secrets for the api resource application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The api resource application identifier.</param>
		/// <param name="secrets">The list of secrets.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Secret>>> SaveSecretsAsync(Guid applicationID, IEnumerable<Secret> secrets)
		{
			ApiResource application = new ApiResource()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IApiResourceValidator> validator = this.validationServiceFactory.GetFactory<IApiResourceValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<Secret>>(Enumerable.Empty<Secret>())
					{
						Errors = result.Errors
					};
				}

				IApiResourceDocumentRepository repository = this.repositoryFactory.GetService<IApiResourceDocumentRepository>();

				ApiResourceDocument document = this.mapper.MapNew<ApiResource, ApiResourceDocument>(application);
				document.Secrets = this.mapper.MapNew<Secret, SecretDocument>(secrets ?? Enumerable.Empty<Secret>());

				document = await repository.SaveSecrets(document);
				secrets = this.mapper.MapNew<SecretDocument, Secret>(document.Secrets ?? Enumerable.Empty<SecretDocument>());

				scope.Complete();

				return new ServiceResult<IEnumerable<Secret>>(secrets);
			}
		}

		/// <summary>
		/// Updates existing api resource <paramref name="application" />.
		/// </summary>
		/// <param name="application">The api resource application.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<ApiResource>> UpdateApplicationAsync(ApiResource application)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<ApiResource> serviceResult = await this.GetApplicationAsync(application.ID);
				if (!serviceResult.IsSuccess)
				{
					return serviceResult;
				}

				this.mapper.Merge(serviceResult.Data, application);

				ApiResourceDocument document = this.mapper.MapNew<ApiResource, ApiResourceDocument>(application);
				document = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().SaveAsync(document);
				application = this.mapper.Map(document, application);

				scope.Complete();
			}

			return new ServiceResult<ApiResource>(application);
		}

		/// <summary>
		/// Deletes the resource application by specified resource application identifier.
		/// </summary>
		/// <param name="id">The resource application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<ApiResource>> DeleteApplicationAsync(Guid id)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<ApiResource> result = await this.GetApplicationAsync(id);
				if (!result.IsSuccess)
				{
					return result;
				}

				await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().DeleteAsync(result.Data.ID);

				scope.Complete();

				return result;
			}
		}

		#endregion
	}
}
