namespace HomeCloud.Data.SqlServer
{
	/// <summary>
	/// Defines execution of operations against the <see cref="SqlServer" /> database in a single scope.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IDataContextScope" />
	public interface ISqlServerDBContextScope : IDataContextScope
	{
		/// <summary>
		/// Gets the <see cref="ISqlServerDBRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="ISqlServerDBRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		T GetRepository<T>() where T : ISqlServerDBRepository;
	}
}
