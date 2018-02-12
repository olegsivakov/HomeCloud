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
		/// Adds default <see cref="ISqlServerDBContextScope" /> data context scope to the service collection.
		/// </summary>
		/// <returns>
		/// The instance of <see cref="ISqlServerDBBuilder" />.
		/// </returns>
		ISqlServerDBBuilder AddContextScope();

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
		/// Adds specified <see cref="ISqlServerDBContextScope" /> data context to the service collection.
		/// </summary>
		/// <typeparam name="TContextScope">The type of the data context derived from <see cref="ISqlServerDBContextScope" />.</typeparam>
		/// <typeparam name="TImplementation">The type of <see cref="ISqlServerDBContextScope" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="ISqlServerDBBuilder" />.
		/// </returns>
		ISqlServerDBBuilder AddContextScope<TContextScope, TImplementation>()
			where TContextScope : class, ISqlServerDBContextScope
			where TImplementation : SqlServerDBContextScope, TContextScope;

		/// <summary>
		/// Adds the specified <see cref="ISqlServerDBRepository{TContract}" /> repository to the service collection.
		/// </summary>
		/// <typeparam name="TRepository">The type of the repository derived from <see cref="ISqlServerDBRepository{TContract}" />.</typeparam>
		/// <typeparam name="TContract">The type of the contract handled by the repository.</typeparam>
		/// <typeparam name="TImplementation">The type of the <see cref="ISqlServerDBRepository{TContract}" /> implementation.</typeparam>
		/// <returns>
		/// The instance of <see cref="ISqlServerDBBuilder" />.
		/// </returns>
		ISqlServerDBBuilder AddRepository<TRepository, TContract, TImplementation>()
			where TRepository : class, ISqlServerDBRepository<TContract>
			where TImplementation : class, TRepository;
	}
}
