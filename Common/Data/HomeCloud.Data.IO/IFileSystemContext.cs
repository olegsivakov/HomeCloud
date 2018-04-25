namespace HomeCloud.Data.IO
{
	#region Usings

	using System;
	using System.Transactions;

	using HomeCloud.IO;

	#endregion

	/// <summary>
	/// Defines methods to query data from file system.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	public interface IFileSystemContext : IFileOperations, IDisposable
	{
		/// <summary>
		/// Creates the file system transaction scope.
		/// </summary>
		/// <returns>The instance of <see cref="TransactionScope"/>.</returns>
		TransactionScope CreateTransaction();

		/// <summary>
		/// Commits the changes to the file system.
		/// </summary>
		void Commit();
	}
}
