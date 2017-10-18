namespace HomeCloud.DataAccess.Contracts
{
	/// <summary>
	/// Defines methods to execute database queries within transaction.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Contracts.IDbContext" />
	public interface ITransactionalDbContext : IDbContext
	{
		/// <summary>
		/// Commits the existing changes in database.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollbacks the existing changes made in database.
		/// </summary>
		void Rollback();
	}
}
