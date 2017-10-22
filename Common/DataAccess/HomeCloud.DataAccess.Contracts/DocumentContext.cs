namespace HomeCloud.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using MongoDB.Driver;

	#endregion

	/// <summary>
	/// Provides methods to query documents from data sources.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Contracts.IDocumentContext" />
	public class DocumentContext : IDocumentContext
	{
		#region  Private Members

		/// <summary>
		/// The document database client member.
		/// </summary>
		private static readonly IMongoClient client = new MongoClient();

		/// <summary>
		/// The document database member.
		/// </summary>
		private static IMongoDatabase database = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentContext" /> class.
		/// </summary>
		/// <param name="databaseName">The database name.</param>
		public DocumentContext(string databaseName)
		{
			database = client.GetDatabase(databaseName);
		}

		#endregion

		#region IDocumentContext Implementations

		/// <summary>
		/// Inserts the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="document">The document.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task InsertAsync<TDocument>(TDocument document)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = database.GetCollection<TDocument>(nameof(TDocument));

			await collection.InsertOneAsync(document);
		}

		/// <summary>
		/// Updates the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="document">The document.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task UpdateAsync<TDocument>(TDocument document)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = database.GetCollection<TDocument>(nameof(TDocument));

			await collection.FindOneAndReplaceAsync(data => data.ID == document.ID, document);
		}

		/// <summary>
		/// Looks for the documents by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The document selector.</param>
		/// <returns>
		/// The asynchronous operation that returns the list of <see cref="TDocument" />.
		/// </returns>
		public async Task<IEnumerable<TDocument>> FindAsync<TDocument>(Expression<Func<TDocument, bool>> selector)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = database.GetCollection<TDocument>(nameof(TDocument));

			return await collection.Find(selector).ToListAsync();
		}

		/// <summary>
		/// Deletes the documents found by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The document selector.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task DeleteAsync<TDocument>(Expression<Func<TDocument, bool>> selector)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = database.GetCollection<TDocument>(nameof(TDocument));

			await collection.DeleteManyAsync(selector);
		}

		#endregion
	}
}
