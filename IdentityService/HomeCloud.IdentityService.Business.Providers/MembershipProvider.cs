namespace HomeCloud.IdentityService.Business.Providers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;
	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.Mapping;

	#endregion

	public class MembershipProvider : IMembershipProvider
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
		/// Initializes a new instance of the <see cref="MembershipProvider"/> class.
		/// </summary>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public MembershipProvider(
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IMembershipProvider  Implementations

		public async Task<User> CreateUser(User user)
		{
			throw new NotImplementedException();
		}

		public async Task<User> DeleteUser(User user)
		{
			throw new NotImplementedException();
		}

		public async Task<IPaginable<User>> GetAllUsers(int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<string>> GetRoles()
		{
			throw new NotImplementedException();
		}

		public async Task<IPaginable<User>> GetUsersInRole(Role role, int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public async Task<User> UpdateUser(User user)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UserExists(User user)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ValidateUser(string username, string password)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
