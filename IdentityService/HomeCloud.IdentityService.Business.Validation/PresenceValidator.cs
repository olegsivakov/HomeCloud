namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;
	using HomeCloud.Exceptions;

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Entities.Membership;

	using HomeCloud.IdentityService.DataAccess;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to validate whether the specified instance already exists.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Guid}" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IPresenceValidator" />
	public class PresenceValidator : Validator<Guid>, IPresenceValidator
	{
		#region Private Members

		/// <summary>
		/// The <see cref="IServiceFactory{IMongoDBRepository}"/> factory.
		/// </summary>
		private readonly IServiceFactory<IMongoDBRepository> repositoryFactory = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PresenceValidator" /> class.
		/// </summary>
		/// <param name="repositoryFactory">The <see cref="IServiceFactory{IMongoDBRepository}"/> factory.</param>
		public PresenceValidator(IServiceFactory<IMongoDBRepository> repositoryFactory)
			: base()
		{
			this.repositoryFactory = repositoryFactory;
		}

		#endregion

		#region IPresenceValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Client"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Client instance)
		{
			this.If(async id => (await this.repositoryFactory.GetService<IClientDocumentRepository>().GetAsync(id)) is null).AddError(new NotFoundException("Specified client application does not exist."));

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="ApiResource"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(ApiResource instance)
		{
			this.If(async id => (await this.repositoryFactory.GetService<IApiResourceDocumentRepository>().GetAsync(id)) is null).AddError(new NotFoundException("Specified api resource does not exist."));

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Grant"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Grant instance)
		{
			this.If(async id => (await this.repositoryFactory.GetService<IGrantDocumentRepository>().GetAsync(instance.ID)) is null).AddError(new NotFoundException("Specified grant does not exist."));

			return await this.ValidateAsync(Guid.Empty);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="User"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(User instance)
		{
			if (instance.ID != Guid.Empty)
			{
				this.If(async id => (await this.repositoryFactory.GetService<IUserDocumentRepository>().GetAsync(id)) is null).AddError(new NotFoundException("Specified user does not exist."));
			}

			if (!string.IsNullOrWhiteSpace(instance.Username))
			{
				this.If(async id => (await this.repositoryFactory.GetService<IUserDocumentRepository>().GetAsync(instance.Username)) is null).AddError(new NotFoundException("Specified user does not exist."));
			}

			return await this.ValidateAsync(Guid.Empty);
		}

		#endregion
	}
}
