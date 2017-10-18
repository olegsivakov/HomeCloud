namespace HomeCloud.DataAccess.Services.Factories
{
	/// <summary>
	/// Defines methods to distribute query handler factories.
	/// </summary>
	public interface IQueryHandlerFactory
	{
		/// <summary>
		/// Gets the query handler factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="IQueryHandlerFactory"/>.</typeparam>
		/// <returns>The instance of <see cref="IQueryHandlerFactory"/>.</returns>
		T GetFactory<T>() where T : IQueryHandlerFactory;
	}
}
