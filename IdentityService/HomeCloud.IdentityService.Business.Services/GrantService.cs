namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using System.Transactions;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

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
		/// Deletes the list of <paramref name="grants" />.
		/// </summary>
		/// <param name="grants"></param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IEnumerable<Grant>>> DeleteGrants(IEnumerable<Grant> grants)
		{
			if (!grants.Any())
			{
				return new ServiceResult<IEnumerable<Grant>>(grants);
			}

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				IServiceFactory<IClientValidator> validator = this.validationServiceFactory.GetFactory<IClientValidator>();
				ValidationResult validationResult = null;
				List<Grant> result = new List<Grant>();

				IEnumerable<IGrouping<Guid, Grant>> groups = grants.GroupBy(grant => grant.ClientID);
			
				groups.ForEachAsync(async group =>
				{
					Client client = new Client() { ID = group.Key };

					ValidationResult clientResult = await validator.Get<IPresenceValidator>().ValidateAsync(client);
					if (clientResult.IsValid)
					{
						result.AddRange(await this.DeleteClientGrants(client, grants));
					}

					validationResult += clientResult;
				}, groups.Count());

				scope.Complete();

				return await Task.FromResult(new ServiceResult<IEnumerable<Grant>>(result)
				{
					Errors = validationResult.Errors
				});
			}
		}

		/// <summary>
		/// Gets the list of client application grants by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria">The <see cref="T:HomeCloud.IdentityService.Business.Entities.Grant" /> search criteria.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IPaginable<Grant>>> FindGrants(Grant criteria, int offset = 0, int limit = 20)
		{
			IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
			IEnumerable<GrantDocument> documents = await repository.FindGrants((client, grant) =>
			{
				bool result = true;

				result &= string.IsNullOrWhiteSpace(criteria.ID) || grant.ID == criteria.ID;
				result &= criteria.ClientID == Guid.Empty || (client?.ID).GetValueOrDefault() == criteria.ClientID;
				result &= !criteria.UserID.HasValue || grant.UserID.GetValueOrDefault() == criteria.UserID.GetValueOrDefault();
				result &= string.IsNullOrWhiteSpace(criteria.Type) || grant.Type == criteria.Type;
				result &= string.IsNullOrWhiteSpace(criteria.Data) || grant.Data == criteria.Data;

				return result;
			});

			IEnumerable<Grant> grants = this.mapper.MapNew<GrantDocument, Grant>(documents.Skip(offset).Take(limit));
			return new ServiceResult<IPaginable<Grant>>(new PagedList<Grant>(grants)
			{
				Offset = offset,
				Limit = limit,
				TotalCount = documents.Count()
			});
		}

		/// <summary>
		/// Gets the grant by specified grant identifier.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<Grant>> GetGrant(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return new ServiceResult<Grant>(null);
			}

			IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
			IEnumerable<GrantDocument> documents = await repository.FindGrants((client, grant) => grant.ID == id);

			Grant result = this.mapper.MapNew<GrantDocument, Grant>(documents.FirstOrDefault());

			return new ServiceResult<Grant>(result);
		}

		/// <summary>
		/// Gets the list of available grant types.
		/// </summary>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IDictionary<int, string>>> GetGrantTypes()
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
		public async Task<ServiceResult<Grant>> SaveGrant(Grant grant)
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
				client.Grants = await repository.FindGrants((clientDocument, grantDocument) => clientDocument.ID == client.ID);

				GrantDocument document = client.Grants.FirstOrDefault(item =>
				{
					bool result = string.IsNullOrWhiteSpace(grant.ID) || item.ID == grant.ID;

					return result |= item.ClientID == grant.ClientID && item.Type == grant.Type && item.UserID.GetValueOrDefault() == grant.UserID.GetValueOrDefault();
				});

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

		#region Private Methods

		/// <summary>
		/// Deletes the list of grants of the specified client.
		/// </summary>
		/// <param name="client">The client which grants should be deleted.</param>
		/// <param name="grants">The list of grants to delete.</param>
		/// <returns>The list of deleted instances of <see cref="Grant"/>.</returns>
		private async Task<IEnumerable<Grant>> DeleteClientGrants(Client client, IEnumerable<Grant> grants)
		{
			ClientDocument document = this.mapper.MapNew<Client, ClientDocument>(client);

			IClientDocumentRepository repository = this.repositoryFactory.GetService<IClientDocumentRepository>();
			document.Grants = await repository.FindGrants((clientDocument, grantDocument) => clientDocument.ID == document.ID);

			IEnumerable<GrantDocument> grantsToDelete = document.Grants.Where(grant => grants.Any(item => item.ID == grant.ID));
			document.Grants = document.Grants.Except(grantsToDelete);

			document = await repository.SaveGrants(document);

			return this.mapper.MapNew<GrantDocument, Grant>(grantsToDelete);
		}

		#endregion
	}
}
