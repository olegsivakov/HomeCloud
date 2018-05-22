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
	using HomeCloud.IdentityService.Business.Validation;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to manage client application grants.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Services.IGrantService" />
	public class GrantService : IGrantService
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
		/// Initializes a new instance of the <see cref="GrantService" /> class.
		/// </summary>
		/// <param name="validationServiceFactory">The validation service factory.</param>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public GrantService(
			IValidationServiceFactory validationServiceFactory,
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.validationServiceFactory = validationServiceFactory;
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IGrantService Implementations

		/// <summary>
		/// Searches for the list of <paramref name="grants" /> and deletes them by specified <paramref name="criteria" />. If <paramref name="criteria" /> is not set or empty no grants will be deleted.
		/// </summary>
		/// <param name="criteria">The <see cref="T:HomeCloud.IdentityService.Business.Entities.GrantSearchCriteria" /> search criteria.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Grant>>> DeleteGrantsAsync(GrantSearchCriteria criteria)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<IEnumerable<Grant>> searchResult = await this.FindGrantsAsync(criteria);
				if (!searchResult.IsSuccess)
				{
					return searchResult;
				}

				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();

				IEnumerable<IGrouping<Guid, Grant>> groups = searchResult.Data.GroupBy(item => item.ClientID);
				foreach (IGrouping<Guid, Grant> group in groups)
				{
					ClientDocument client = new ClientDocument()
					{
						ID = group.Key,
						Grants = await repository.FindGrants(document => document.ID == group.Key, document => group.Any(item => item.ID == document.ID))
					};

					client = await repository.SaveGrants(client);
				}

				scope.Complete();

				return searchResult;
			}
		}

		/// <summary>
		/// Deletes the grant by specified grant identifier.
		/// </summary>
		/// <param name="id">The grant identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Grant>> DeleteGrantAsync(string id)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<Grant> result = await this.GetGrantAsync(id);
				if (!result.IsSuccess)
				{
					return result;
				}

				ClientDocument client = new ClientDocument()
				{
					ID = result.Data.ClientID
				};

				await this.repositoryFactory.GetService<IClientDocumentRepository>().DeleteGrant(client, result.Data.ID);

				scope.Complete();

				return result;
			}
		}

		/// <summary>
		/// Gets the list of client application grants by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The <see cref="T:HomeCloud.IdentityService.Business.Entities.Grant" /> search criteria.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Grant>>> FindGrantsAsync(GrantSearchCriteria criteria)
		{
			if (criteria is null)
			{
				return new ServiceResult<IEnumerable<Grant>>(Enumerable.Empty<Grant>());
			}

			IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
			IEnumerable<GrantDocument> documents = await repository.FindGrants(
				client => !criteria.ClientID.HasValue || client.ID == criteria.ClientID.Value,
				grant =>
				!criteria.UserID.HasValue || grant.UserID == criteria.UserID.GetValueOrDefault()
				&&
				string.IsNullOrWhiteSpace(criteria.Type) || grant.Type == criteria.Type);

			IEnumerable<Grant> grants = this.mapper.MapNew<GrantDocument, Grant>(documents);

			return new ServiceResult<IEnumerable<Grant>>(grants);
		}

		/// <summary>
		/// Gets the grant by specified grant identifier.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Grant>> GetGrantAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return new ServiceResult<Grant>(null);
			}

			IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
			IEnumerable<GrantDocument> documents = await repository.FindGrants(null, grant => grant.ID == id);

			Grant result = this.mapper.MapNew<GrantDocument, Grant>(documents.FirstOrDefault());

			return new ServiceResult<Grant>(result);
		}

		/// <summary>
		/// Gets the list of available grant types.
		/// </summary>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IDictionary<int, string>>> GetGrantTypesAsync()
		{
			IDictionary<int, string> result = Enum.GetValues(typeof(GrantType)).Cast<int>().ToDictionary(value => value, value => ((GrantType)value).ToString());

			return await Task.FromResult(new ServiceResult<IDictionary<int, string>>(result));
		}

		/// <summary>
		/// Saves the client application <paramref name="grant" />.
		/// </summary>
		/// <param name="grant">The grant to save.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Grant>> SaveGrantAsync(Grant grant)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IGrantValidator> validator = this.validationServiceFactory.GetFactory<IGrantValidator>();
				ValidationResult validationResult = await validator.Get<IRequiredValidator>().ValidateAsync(grant);
				if (!validationResult.IsValid)
				{
					return new ServiceResult<Grant>(grant)
					{
						Errors = validationResult.Errors
					};
				}

				ClientDocument client = new ClientDocument()
				{
					ID = grant.ClientID
				};

				IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
				client.Grants = await repository.FindGrants(clientDocument => clientDocument.ID == client.ID, null);

				GrantDocument document = client.Grants.FirstOrDefault(item => item.ID == grant.ID);
				if (document is null)
				{
					document = this.mapper.MapNew<Grant, GrantDocument>(grant);
					client.Grants.Union(new List<GrantDocument>() { document });
				}
				else
				{
					Grant existingGrant = this.mapper.MapNew<GrantDocument, Grant>(document);
					this.mapper.Merge(existingGrant, grant);

					this.mapper.Map(grant, document);
				}

				client = await repository.SaveGrants(client);

				scope.Complete();

				document = client.Grants.FirstOrDefault(item => item.ID == grant.ID);
				grant = this.mapper.Map(document, grant);

				return new ServiceResult<Grant>(grant);
			}
		}

		#endregion
	}
}
