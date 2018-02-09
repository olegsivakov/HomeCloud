namespace HomeCloud.Data.SqlServer
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines execution of operations against the <see cref="SqlServer"/> database in a single scope.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface ISqlServerDBContextScope : IDisposable
	{
		/// <summary>
		/// Gets the <see cref="ISqlServerDBRepository<T>"/> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="ISqlServerDBRepository"/>.</typeparam>
		/// <returns>The instance of <see cref="ISqlServerDBRepository"/>.</returns>
		ISqlServerDBRepository<T> GetRepository<T>();

		/// <summary>
		/// Commits the changes made in database.
		/// </summary>
		void Commit();
	}
}
