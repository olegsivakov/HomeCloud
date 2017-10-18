namespace HomeCloud.DataAccess.Services
{
	#region Usings

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Marks the handler to executes database commands.
	/// </summary>
	public interface IDbCommandHandler
	{
	}

	/// <summary>
	/// Defines combination of methods to execute commands against database.
	/// </summary>
	/// <typeparam name="TDbCommand">The type of the database command.</typeparam>
	public interface IDbCommandHandler<TDbCommand> : IDbCommandHandler where TDbCommand : IDbCommand
	{
		/// <summary>
		/// Executes the specified command against database.
		/// </summary>
		/// <param name="command">The database command.</param>
		void Execute(TDbCommand command);
	}
}
