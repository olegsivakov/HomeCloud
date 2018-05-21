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
	/// Provides methods to manage client applications.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Services.IClientService" />
	public class ClientService : IClientService
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
		/// Initializes a new instance of the <see cref="ClientService" /> class.
		/// </summary>
		/// <param name="validationServiceFactory">The validation service factory.</param>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public ClientService(
			IValidationServiceFactory validationServiceFactory,
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.validationServiceFactory = validationServiceFactory;
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IClientService Implementations

		/// <summary>
		/// Creates new client <paramref name="application" />.
		/// </summary>
		/// <param name="application">The client application.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Client>> CreateApplicationAsync(Client application)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				application.ID = Guid.Empty;

				IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();

				ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(application);
				result += await validator.Get<IUniqueValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<Client>(application)
					{
						Errors = result.Errors
					};
				}

				ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(application);
				document = await this.repositoryFactory.GetService<IClientDocumentRepository>().SaveAsync(document);
				application = this.mapper.Map(document, application);

				scope.Complete();
			}

			return new ServiceResult<Client>(application);
		}

		/// <summary>
		/// Searches for the list of client applications by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The search criteria.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IPaginable<Client>>> FindApplicationsAsync(Client criteria, int offset = 0, int limit = 20)
		{
			IEnumerable<ClientDocument> documents = await this.repositoryFactory.GetService<IClientDocumentRepository>().FindAllAsync(document =>
				criteria == null
				||
				(
					(string.IsNullOrWhiteSpace(criteria.Name) || document.Name.Trim().ToLower().Contains(criteria.Name.Trim().ToLower()))
					&&
					(criteria.GrantType == GrantType.Unknown || document.GrantType == (int)criteria.GrantType)
				));

			IEnumerable<Client> result = this.mapper.MapNew<ClientDocument, Client>(documents.Skip(offset).Take(limit));

			return new ServiceResult<IPaginable<Client>>(new PagedList<Client>(result)
			{
				Offset = offset,
				Limit = limit,
				TotalCount = documents.Count()
			});
		}

		/// <summary>
		/// Gets client application by specified client application identifier.
		/// </summary>
		/// <param name="id">The client identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Client>> GetApplicationAsync(Guid id)
		{
			Client application = new Client()
			{
				ID = id
			};

			IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<Client>(application)
				{
					Errors = result.Errors
				};
			}

			ClientDocument document = await this.repositoryFactory.GetService<IClientDocumentRepository>().GetAsync(application.ID);
			application = this.mapper.Map(document, application);

			return new ServiceResult<Client>(application);
		}

		/// <summary>
		/// Updates existing client <paramref name="application" />.
		/// </summary>
		/// <param name="application">The client application.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Client>> UpdateApplicationAsync(Client application)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<Client> serviceResult = await this.GetApplicationAsync(application.ID);
				if (!serviceResult.IsSuccess)
				{
					return serviceResult;
				}

				this.mapper.Merge(serviceResult.Data, application);

				ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(application);
				document = await this.repositoryFactory.GetService<IClientDocumentRepository>().SaveAsync(document);
				application = this.mapper.Map(document, application);

				scope.Complete();
			}

			return new ServiceResult<Client>(application);
		}

		/// <summary>
		/// Gets the list of application grants by specified client application identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Grant>>> GetGrantsAsync(Guid applicationID)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<Grant>>(Enumerable.Empty<Grant>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<GrantDocument> documents = await this.repositoryFactory.GetService<IClientDocumentRepository>().FindGrants((client, grant) => client.ID == application.ID);
			IEnumerable<Grant> grants = this.mapper.MapNew<GrantDocument, Grant>(documents);

			return new ServiceResult<IEnumerable<Grant>>(grants);

		}

		/// <summary>
		/// Gets the list of application origins by specified client application identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> GetOriginsAsync(Guid applicationID)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<string> documents = await this.repositoryFactory.GetService<IClientDocumentRepository>().FindOrigins((client, origin) => client.ID == application.ID);

			return new ServiceResult<IEnumerable<string>>(documents);
		}

		/// <summary>
		/// Gets the list of application scopes by specified client application identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> GetScopesAsync(Guid applicationID)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<string> documents = await this.repositoryFactory.GetService<IClientDocumentRepository>().FindScopes((client, scope) => client.ID == application.ID);

			return new ServiceResult<IEnumerable<string>>(documents);
		}

		/// <summary>
		/// Gets the list of application secrets by specified client application identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Secret>>> GetSecretsAsync(Guid applicationID)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

			if (!result.IsValid)
			{
				return new ServiceResult<IEnumerable<Secret>>(Enumerable.Empty<Secret>())
				{
					Errors = result.Errors
				};
			}

			IEnumerable<SecretDocument> documents = await this.repositoryFactory.GetService<IClientDocumentRepository>().FindSecrets((client, secret) => client.ID == application.ID);
			IEnumerable<Secret> secrets = this.mapper.MapNew<SecretDocument, Secret>(documents);

			return new ServiceResult<IEnumerable<Secret>>(secrets);
		}

		/// <summary>
		/// Saves the list of application grants for the client application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <param name="grants">The list of grants.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Grant>>> SaveGrantsAsync(Guid applicationID, IEnumerable<Grant> grants)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<Grant>>(Enumerable.Empty<Grant>())
					{
						Errors = result.Errors
					};
				}

				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();

				ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(application);
				document.Grants = this.mapper.MapNew<Grant, GrantDocument>(grants ?? Enumerable.Empty<Grant>());

				document = await repository.SaveGrants(document);
				grants = this.mapper.MapNew<GrantDocument, Grant>(document.Grants ?? Enumerable.Empty<GrantDocument>());

				scope.Complete();

				return new ServiceResult<IEnumerable<Grant>>(grants);
			}

		}

		/// <summary>
		/// Saves the list of application origins for the client application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <param name="origins">The list of origins.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> SaveOriginsAsync(Guid applicationID, IEnumerable<string> origins)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
					{
						Errors = result.Errors
					};
				}

				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();

				ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(application);
				document.Origins = origins ?? Enumerable.Empty<string>();

				document = await repository.SaveOrigins(document);

				scope.Complete();

				return new ServiceResult<IEnumerable<string>>(document.Origins);
			}
		}

		/// <summary>
		/// Saves the list of application scopes for the client application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <param name="scopes">The list of scopes.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<string>>> SaveScopesAsync(Guid applicationID, IEnumerable<string> scopes)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<string>>(Enumerable.Empty<string>())
					{
						Errors = result.Errors
					};
				}

				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();

				ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(application);
				document.Scopes = scopes ?? Enumerable.Empty<string>();

				document = await repository.SaveScopes(document);

				scope.Complete();

				return new ServiceResult<IEnumerable<string>>(document.Scopes);
			}
		}

		/// <summary>
		/// Saves the list of application secrets for the client application specified by identifier.
		/// </summary>
		/// <param name="applicationID">The client application identifier.</param>
		/// <param name="secrets">The list of secrets.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Secret>>> SaveSecretsAsync(Guid applicationID, IEnumerable<Secret> secrets)
		{
			Client application = new Client()
			{
				ID = applicationID
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
				ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(application);

				if (!result.IsValid)
				{
					return new ServiceResult<IEnumerable<Secret>>(Enumerable.Empty<Secret>())
					{
						Errors = result.Errors
					};
				}

				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();

				ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(application);
				document.Secrets = this.mapper.MapNew<Secret, SecretDocument>(secrets ?? Enumerable.Empty<Secret>());

				document = await repository.SaveSecrets(document);
				secrets = this.mapper.MapNew<SecretDocument, Secret>(document.Secrets ?? Enumerable.Empty<SecretDocument>());

				scope.Complete();

				return new ServiceResult<IEnumerable<Secret>>(secrets);
			}
		}

		/// <summary>
		/// Deletes the client application by specified client application identifier.
		/// </summary>
		/// <param name="id">The client application identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Client>> DeleteApplicationAsync(Guid id)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<Client> result = await this.GetApplicationAsync(id);
				if (!result.IsSuccess)
				{
					return result;
				}

				await this.repositoryFactory.GetService<IClientDocumentRepository>().DeleteAsync(result.Data.ID);

				scope.Complete();

				return result;
			}
		}

		#endregion
	}
}
