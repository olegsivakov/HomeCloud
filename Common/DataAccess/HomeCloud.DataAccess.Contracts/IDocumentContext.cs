namespace HomeCloud.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Defines methods to query documents from data sources.
	/// </summary>
	public interface IDocumentContext
	{
		/// <summary>
		/// Inserts the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="document">The document.</param>
		/// <returns>The asynchronous operation.</returns>
		Task InsertAsync<TDocument>(TDocument document)
			where TDocument : IDocument;

		/// <summary>
		/// Updates the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="document">The document.</param>
		/// <returns>The asynchronous operation.</returns>
		Task UpdateAsync<TDocument>(TDocument document)
			where TDocument : IDocument;

		/// <summary>
		/// Looks for the documents by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The document selector.</param>
		/// <returns>The asynchronous operation that returns the list of <see cref="TDocument"/>.</returns>
		Task<IEnumerable<TDocument>> FindAsync<TDocument>(Expression<Func<TDocument, bool>> selector)
			where TDocument : IDocument;

		/// <summary>
		/// Deletes the documents found by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The document selector.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteAsync<TDocument>(Expression<Func<TDocument, bool>> selector)
			where TDocument : IDocument;
	}
}
