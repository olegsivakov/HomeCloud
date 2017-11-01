namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Defines methods to create the validation factories of <see cref="IServiceFactory{T}"/> type.
	/// </summary>
	public interface IValidationServiceFactory
	{
		/// <summary>
		/// Gets the validation factory of <see cref="IServiceFactory{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of validator which creation is handled by <see cref="IServiceFactory{T}"/>.</typeparam>
		/// <returns>The instance of <see cref="IServiceFactory{T}"/>.</returns>
		IServiceFactory<T> GetFactory<T>()
			where T : IValidator;
	}
}
