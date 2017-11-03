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
	public interface IDocumentContext : IDisposable
	{
		/// <summary>
		/// Inserts the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document to insert.</typeparam>
		/// <param name="document">The document to insert.</param>
		/// <returns>The asynchronous operation.</returns>
		Task InsertAsync<TDocument>(TDocument document)
			where TDocument : IDocument;

		/// <summary>
		/// Updates the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document to update.</typeparam>
		/// <param name="document">The document to update.</param>
		/// <returns>The asynchronous operation that returns the instance of <see cref="TDocument" />..</returns>
		Task<TDocument> UpdateAsync<TDocument>(TDocument document)
			where TDocument : IDocument;

		/// <summary>
		/// Updates the existing document asynchronously or creates a new one if no documents matched.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document to upsert.</typeparam>
		/// <param name="document">The document to upsert.</param>
		/// <returns>The asynchronous operation that returns the instance of <see cref="TDocument" />..</returns>
		Task<TDocument> UpsertAsync<TDocument>(TDocument document)
			where TDocument : IDocument;

		/// <summary>
		/// Looks for the first document in the result set by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <returns>The asynchronous operation that returns the instance of <see cref="TDocument" />.</returns>
		Task<TDocument> GetAsync<TDocument>(Expression<Func<TDocument, bool>> selector)
			where TDocument : IDocument;

		/// <summary>
		/// Looks for the documents by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The document selector.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The asynchronous operation that returns the list of <see cref="TDocument" />.
		/// </returns>
		Task<IEnumerable<TDocument>> FindAsync<TDocument>(Expression<Func<TDocument, bool>> selector, int? offset = null, int? limit = null)
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
