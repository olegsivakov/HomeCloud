namespace HomeCloud.Data.SqlServer
{
	/// <summary>
	/// Defines methods to add <see cref="SqlServer"/> database services to service collection.
	/// </summary>
	public interface ISqlServerDBBuilder
	{
		/// <summary>
		/// Adds default <see cref="ISqlServerDBContext" /> data context to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="ISqlServerDBBuilder" />.
		/// </returns>
		ISqlServerDBBuilder AddContext();

		/// <summary>
		/// Adds specified <see cref="ISqlServerDBContext" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The type of the data context derived from <see cref="ISqlServerDBContext" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="ISqlServerDBContext" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="ISqlServerDBBuilder" />.
		/// </returns>
		ISqlServerDBBuilder AddContext<TContext, TImplementation>()
			where TContext : class, ISqlServerDBContext
			where TImplementation : SqlServerDBContext, TContext;

		/// <summary>
		/// Adds the specified <see cref="ISqlServerDBRepository" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="ISqlServerDBRepository" />.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="ISqlServerDBRepository" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="ISqlServerDBBuilder" />.
		/// </returns>
		ISqlServerDBBuilder AddRepository<TRepository, TImplementation>()
			where TRepository : class, ISqlServerDBRepository
			where TImplementation : class, TRepository;
	}
}
