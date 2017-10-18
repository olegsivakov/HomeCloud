namespace HomeCloud.DataAccess.Services.Factories
{
	/// <summary>
	/// Defines methods to distribute repository factories.
	/// </summary>
	public interface IRepositoryFactory
	{
		/// <summary>
		/// Gets the repository factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="IRepositoryFactory"/>.</typeparam>
		/// <returns>The instance of <see cref="IRepositoryFactory"/>.</returns>
		T GetFactory<T>() where T : IRepositoryFactory;
	}
}
