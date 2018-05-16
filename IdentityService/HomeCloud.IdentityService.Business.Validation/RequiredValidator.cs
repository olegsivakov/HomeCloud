namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Membership;

	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to validate required attributes of the instance.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Object}" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IRequiredValidator" />
	public class RequiredValidator : Validator<object>, IRequiredValidator
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredValidator" /> class.
		/// </summary>
		public RequiredValidator()
			: base()
		{
			this.If(obj => obj is null).AddError("The instance is not defined.");
		}

		#endregion

		#region IRequiredValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Client"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Client instance)
		{
			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The specified client application name is empty.");
			this.If(obj => instance.GrantType == GrantType.Unknown).AddError("The specified client application grant type is unknown.");

			return await this.ValidateAsync((object)instance);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="ApiResource"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(ApiResource instance)
		{
			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The specified api resource name is empty.");

			return await this.ValidateAsync((object)instance);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Grant"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Grant instance)
		{
			this.If(obj => instance.ClientID == Guid.Empty).AddError("The client application identifier of the specified grant is empty.");
			this.If(obj => string.IsNullOrWhiteSpace(instance.Type)).AddError("The type of the specified grant is empty.");
			this.If(obj => string.IsNullOrWhiteSpace(instance.Data)).AddError("The data of the specified grant is empty.");

			return await this.ValidateAsync((object)instance);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="User"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(User instance)
		{
			this.If(obj => string.IsNullOrWhiteSpace(instance.Username)).AddError("The specified username is empty.");
			this.If(obj => instance.Role == Role.Anonymous).AddError("The user is anonymous.");

			return await this.ValidateAsync((object)instance);
		}

		#endregion
	}
}
