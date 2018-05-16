namespace HomeCloud.IdentityService.Business.Validation
{
	/// <summary>
	/// Defines methods to validate whether the specified instance already exists.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IGrantValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IUserValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IClientValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IApiResourceValidator" />
	public interface IPresenceValidator : IGrantValidator, IUserValidator, IClientValidator, IApiResourceValidator
	{
	}

}
