namespace HomeCloud.IdentityService.Business.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Membership;

	using HomeCloud.Mapping;

	#endregion

	/// <summary>
	/// Provides methods to manage users, roles and grants.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Services.IMembershipService" />
	public class MembershipService : IMembershipService
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
		/// Initializes a new instance of the <see cref="MembershipService"/> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public MembershipService(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IMembershipService Implementations

		public Task<ServiceResult<User>> CreateUser(User user)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult<User>> DeleteUser(User user)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult<IPaginable<User>>> FindUsers(User criteria, int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult<IDictionary<int, string>>> GetRoles()
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult<User>> GetUser(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult<User>> GetUser(string username)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult<User>> UpdateUser(User user)
		{
			throw new NotImplementedException();
		}

		public Task<ServiceResult> ValidateUser(string username, string password)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
