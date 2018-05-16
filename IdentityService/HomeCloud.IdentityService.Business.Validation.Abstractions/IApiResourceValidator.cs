namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Defines methods to validate the instance of <see cref="ApiResource" />.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.IValidator{HomeCloud.IdentityService.Business.Entities.Applications.ApiResource}" />
	public interface IApiResourceValidator : IValidator<ApiResource>
	{
	}
}
