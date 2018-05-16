namespace HomeCloud.IdentityService.Business.Validation
{
	/// <summary>
	/// Defines methods to validate required attributes of the instance.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IGrantValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IUserValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IClientValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IApiResourceValidator" />
	public interface IRequiredValidator : IGrantValidator, IUserValidator, IClientValidator, IApiResourceValidator
	{
	}

}
