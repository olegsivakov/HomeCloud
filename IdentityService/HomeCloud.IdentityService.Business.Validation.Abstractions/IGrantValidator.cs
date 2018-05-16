namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Defines methods to validate the instance of <see cref="Grant" />.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.IValidator{HomeCloud.IdentityService.Business.Entities.Grant}" />
	public interface IGrantValidator : IValidator<Grant>
	{
	}
}
