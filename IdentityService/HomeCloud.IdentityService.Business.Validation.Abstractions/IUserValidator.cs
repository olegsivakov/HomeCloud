namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using HomeCloud.IdentityService.Business.Entities.Membership;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Defines methods to validate the instance of <see cref="User" />.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.IValidator{HomeCloud.IdentityService.Business.Entities.Membership.User}" />
	public interface IUserValidator : IValidator<User>
	{
	}
}
