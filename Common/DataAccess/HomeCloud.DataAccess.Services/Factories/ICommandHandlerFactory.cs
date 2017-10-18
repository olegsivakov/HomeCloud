namespace HomeCloud.DataAccess.Services.Factories
{
	/// <summary>
	/// Defines methods to distribute command handler factories.
	/// </summary>
	public interface ICommandHandlerFactory
	{
		/// <summary>
		/// Gets the command handler factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="ICommandHandlerFactory"/>.</typeparam>
		/// <returns>The instance of <see cref="ICommandHandlerFactory"/>.</returns>
		T GetFactory<T>() where T : ICommandHandlerFactory;
	}
}
