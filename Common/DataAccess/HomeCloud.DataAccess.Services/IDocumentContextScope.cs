namespace HomeCloud.DataAccess.Services
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines methods for operation within document context executed as a single scope.
	/// </summary>
	public interface IDocumentContextScope : IDisposable
	{
		/// <summary>
		/// Gets the <see cref="IDocumentRepository"/> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="IDocumentRepository"/>.</typeparam>
		/// <returns>The instance of <see cref="IDocumentRepository"/>.</returns>
		T GetRepository<T>() where T : IDocumentRepository;
	}
}
