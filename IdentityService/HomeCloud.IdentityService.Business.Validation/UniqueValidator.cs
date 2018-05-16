namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;
	using HomeCloud.Exceptions;

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Entities.Membership;

	using HomeCloud.IdentityService.DataAccess;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to validate whether the specified instance is unique.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Guid}" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IUniqueValidator" />
	public class UniqueValidator : Validator<Guid>, IUniqueValidator
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceFactory{IMongoDBRepository}"/> factory.
		/// </summary>
		private readonly IServiceFactory<IMongoDBRepository> repositoryFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UniqueValidator" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The <see cref="IServiceFactory{IMongoDBRepository}"/> factory.</param>
		public UniqueValidator(IServiceFactory<IMongoDBRepository> repositoryFactory)
			: base()
		{
			this.repositoryFactory = repositoryFactory;
		}

		#endregion

		#region IUniqueValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Client"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Client instance)
		{
			if (!string.IsNullOrWhiteSpace(instance.Name))
			{
				this.If(async id =>
				{
					IEnumerable<ClientDocument> documents = await this.repositoryFactory.GetService<IClientDocumentRepository>().FindAsync(item => item.Name.ToLower() == instance.Name.Trim().ToLower(), 0, 1);

					return documents.Any();
				}).AddError(new AlreadyExistsException("Client application with specified name already exists."));
			}

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="ApiResource"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(ApiResource instance)
		{
			if (!string.IsNullOrWhiteSpace(instance.Name))
			{
				this.If(async id =>
				{
					IEnumerable<ApiResourceDocument> documents = await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().FindAsync(item => item.Name.ToLower() == instance.Name.Trim().ToLower(), 0, 1);

					return documents.Any();
				}).AddError(new AlreadyExistsException("Api resource with specified name already exists."));
			}

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Grant"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Grant instance)
		{
			if (instance.ClientID != Guid.Empty && !string.IsNullOrWhiteSpace(instance.Type) && !string.IsNullOrWhiteSpace(instance.Data))
			{
				this.If(async id =>
				{
					IEnumerable<GrantDocument> documents = await this.repositoryFactory.GetService<IGrantDocumentRepository>().FindAsync(item => item.ClientID == instance.ClientID && item.Type == instance.Type && item.Data == instance.Data, 0, 1);

					return documents.Any();
				}).AddError(new AlreadyExistsException("Grant for specified ClientID with specified type and data already exists."));
			}

			return await this.ValidateAsync(Guid.Empty);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="User"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(User instance)
		{
			if (!string.IsNullOrWhiteSpace(instance.Username))
			{
				this.If(async id =>
				{
					IEnumerable<UserDocument> documents = await this.repositoryFactory.GetService<IUserDocumentRepository>().FindAsync(item => item.Username.ToLower() == instance.Username.Trim().ToLower(), 0, 1);

					return documents.Any();
				}).AddError(new AlreadyExistsException("User with specified username already exists."));
			}

			return await this.ValidateAsync(instance.ID);
		}

		#endregion

	}
