namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Defines methods to validate the instance of <see cref="Storage"/>.
	/// </summary>
	public interface IStorageValidator : IValidator<Storage>
	{
	}
}
