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

	using HomeCloud.IdentityService.Business.Entities.Membership;
	using HomeCloud.IdentityService.Business.Validation;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Mapping;
	using HomeCloud.Mapping.Extensions;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to manage users, roles and grants.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Services.IMembershipService" />
	public class MembershipService : IMembershipService
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
		/// Initializes a new instance of the <see cref="MembershipService" /> class.
		/// </summary>
		/// <param name="validationServiceFactory">The validation service factory.</param>
		/// <param name="repositoryFactory">The repository factory.</param>
		/// <param name="mapper">The mapper.</param>
		public MembershipService(
			IValidationServiceFactory validationServiceFactory,
			IServiceFactory<IMongoDBRepository> repositoryFactory,
			IMapper mapper)
		{
			this.validationServiceFactory = validationServiceFactory;
			this.repositoryFactory = repositoryFactory;
			this.mapper = mapper;
		}

		#endregion

		#region IMembershipService Implementations

		/// <summary>
		/// Creates the <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to create.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<User>> CreateUserAsync(User user)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				user.ID = Guid.Empty;

				IServiceFactory<IUserValidator> validator = this.validationServiceFactory.GetFactory<IUserValidator>();

				ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(user);
				result += await validator.Get<IUniqueValidator>().ValidateAsync(user);

				if (!result.IsValid)
				{
					return new ServiceResult<User>(user)
					{
						Errors = result.Errors
					};
				}

				UserDocument document = this.mapper.MapNew<User, UserDocument>(user);
				document = await this.repositoryFactory.GetService<IUserDocumentRepository>().SaveAsync(document);
				user = this.mapper.Map(document, user);

				scope.Complete();
			}

			return new ServiceResult<User>(user);
		}

		/// <summary>
		/// Deletes the user.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The result of execution of service operation.</returns>
		public async Task<ServiceResult<User>> DeleteUserAsync(Guid id)
		{
			User user = new User()
			{
				ID = id
			};

			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<User> serviceResult = await this.GetUserAsync(id);
				if (!serviceResult.IsSuccess)
				{
					return serviceResult;
				}

				user = serviceResult.Data;

				await this.repositoryFactory.Get<IUserDocumentRepository>().DeleteAsync(user.ID);

				scope.Complete();
			}

			return new ServiceResult<User>(user);
		}

		/// <summary>
		/// Searches for the list of users by specified search <paramref name="criteria" />.
		/// </summary>
		/// <param name="criteria"></param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IPaginable<User>>> FindUsersAsync(User criteria, int offset = 0, int limit = 20)
		{
			IPaginable<UserDocument> documents = await this.repositoryFactory.GetService<IUserDocumentRepository>().FindAsync(item =>
				string.IsNullOrWhiteSpace(criteria.Username) | item.Username.Trim().Contains(criteria.Username.Trim().ToLower())
				&&
				string.IsNullOrWhiteSpace(criteria.FirstName) | item.FirstName.Trim().Contains(criteria.FirstName.Trim().ToLower())
				&&
				string.IsNullOrWhiteSpace(criteria.LastName) | item.LastName.Trim().Contains(criteria.LastName.Trim().ToLower()), offset, limit);

			IEnumerable<User> users = this.mapper.MapNew<UserDocument, User>(documents);
			IPaginable<User> result = new PagedList<User>(users)
			{
				Offset = documents.Offset,
				Limit = documents.Limit,
				TotalCount = documents.TotalCount,
			};

			return new ServiceResult<IPaginable<User>>(result);
		}

		/// <summary>
		/// Gets the list of available user roles.
		/// </summary>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<IDictionary<int, string>>> GetRolesAsync()
		{
			IDictionary<int, string> result = Enum.GetValues(typeof(Role)).Cast<int>().ToDictionary(value => value, value => ((Role)value).ToString());

			return await Task.FromResult(new ServiceResult<IDictionary<int, string>>(result));
		}

		/// <summary>
		/// Gets the user by specified user identifier.
		/// </summary>
		/// <param name="id">The user identifier.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<User>> GetUserAsync(Guid id)
		{
			User user = new User()
			{
				ID = id
			};

			IServiceFactory<IUserValidator> validator = this.validationServiceFactory.GetFactory<IUserValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(user);

			if (!result.IsValid)
			{
				return new ServiceResult<User>(user)
				{
					Errors = result.Errors
				};
			}

			UserDocument document = await this.repositoryFactory.GetService<IUserDocumentRepository>().GetAsync(user.ID);
			user = this.mapper.Map(document, user);

			return new ServiceResult<User>(user);
		}

		/// <summary>
		/// Gets the user by specified <paramref name="username" />.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<User>> GetUserAsync(string username)
		{
			User user = new User()
			{
				Username = username?.Trim()
			};

			IServiceFactory<IUserValidator> validator = this.validationServiceFactory.GetFactory<IUserValidator>();
			ValidationResult result = await validator.Get<IPresenceValidator>().ValidateAsync(user);

			if (!result.IsValid)
			{
				return new ServiceResult<User>(user)
				{
					Errors = result.Errors
				};
			}

			UserDocument document = await this.repositoryFactory.GetService<IUserDocumentRepository>().GetAsync(user.Username);
			user = this.mapper.Map(document, user);

			return new ServiceResult<User>(user);
		}

		/// <summary>
		/// Updates the <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to update.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult<User>> UpdateUserAsync(User user)
		{
			using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
			{
				ServiceResult<User> serviceResult = await this.GetUserAsync(user.ID);
				if (!serviceResult.IsSuccess)
				{
					return serviceResult;
				}

				user.Username = null;
				this.mapper.Merge(serviceResult.Data, user);

				UserDocument document = this.mapper.MapNew<User, UserDocument>(user);
				document = await this.repositoryFactory.GetService<IUserDocumentRepository>().SaveAsync(document);
				user = this.mapper.Map(document, user);

				scope.Complete();
			}

			return new ServiceResult<User>(user);
		}

		/// <summary>
		/// Validates the user specified by <paramref name="username" /> and <paramref name="password" />.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>
		/// The result of execution of service operation.
		/// </returns>
		public async Task<ServiceResult> ValidateUserAsync(string username, string password)
		{
			User user = new User()
			{
				Username = username?.Trim(),
				Password = password
			};

			IServiceFactory<IUserValidator> validator = this.validationServiceFactory.GetFactory<IUserValidator>();

			ValidationResult result = await validator.Get<IRequiredValidator>().ValidateAsync(user);
			result += await validator.Get<IUniqueValidator>().ValidateAsync(user);

			return new ServiceResult()
			{
				Errors = result.Errors
			};
		}

		#endregion
	}
}
