namespace HomeCloud.IdentityService.Business.Validation
{
	/// <summary>
	/// Defines methods to validate whether the specified instance is unique.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IUserValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IClientValidator" />
	/// <seealso cref="HomeCloud.IdentityService.Business.Validation.IApiResourceValidator" />
	public interface IUniqueValidator : IUserValidator, IClientValidator, IApiResourceValidator
	{
	}

}
