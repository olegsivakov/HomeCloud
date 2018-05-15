namespace HomeCloud.IdentityService.Business.Providers
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;

	using HomeCloud.IdentityService.Business.Entities;

	using HomeCloud.Mapping;
	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.Core.Extensions;

	#endregion

	/// <summary>
	/// Provides methods to handle applications.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Providers.IApplicationProvider" />
	public class ApplicationProvider : IApplicationProvider
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
		/// Initializes a new instance of the <see cref="ApplicationProvider"/> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public ApplicationProvider(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IApplicationProvider Implementations

		/// <summary>
		/// Indicates whether the specified application already exists.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns>
		///   <c>True</c> if application exists. Otherwise it returns <c>false</c>.
		/// </returns>
		public Task<bool> ApplicationExists(Application application)
		{
			IApiResourceDocumentRepository apiResourceRepository = this.repositoryFactory.GetService<IApiResourceDocumentRepository>();
			IClientDocumentRepository clientRepository = this.repositoryFactory.GetService<IClientDocumentRepository>();

			bool result = false;

			ParallelExtensions.InvokeAsync(
				async () =>
				{
					result |= (await apiResourceRepository.GetAsync(application.ID)) != null;
				},
				async () =>
				{
					result |= (await clientRepository.GetAsync(application.ID)) != null;
				});

			return Task.FromResult(result);
		}

		public Task<Application> CreateApplication(Application application)
		{
			throw new NotImplementedException();
		}

		public Task<Grant> CreateGrant(Grant grant)
		{
			throw new NotImplementedException();
		}

		public Task<Application> DeleteApplication(Application application)
		{
			throw new NotImplementedException();
		}

		public Task<Grant> DeleteGrant(Grant grant)
		{
			throw new NotImplementedException();
		}

		public Task<IPaginable<Application>> FindApplications(Application application = null, int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public Task<IPaginable<Grant>> FindGrants(Grant grant, int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public Task<Application> GetApplication(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Grant> GetGrant(string id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> GrantExists(Grant grant)
		{
			throw new NotImplementedException();
		}

		public Task<Application> UpdateApplication(Application application)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
