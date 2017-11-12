namespace HomeCloud.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using System.Linq;
	using System.Linq.Expressions;

	using System.Threading.Tasks;

	using MongoDB.Bson;
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
		private static IMongoClient client = null;

		/// <summary>
		/// The document database member.
		/// </summary>
		private static IMongoDatabase database = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentContext" /> class.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		public DocumentContext(string connectionString)
		{
			if (client is null)
			{
				client = new MongoClient(connectionString);
			}

			if (database is null)
			{
				database = client.GetDatabase(MongoUrl.Create(connectionString).DatabaseName);
			}
		}

		#endregion

		#region IDocumentContext Implementations

		/// <summary>
		/// Inserts the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document to insert.</typeparam>
		/// <param name="document">The document to insert.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task InsertAsync<TDocument>(TDocument document)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = await this.GetCollectionAsync<TDocument>();

			await collection.InsertOneAsync(document);
		}

		/// <summary>
		/// Updates the document asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document to update.</typeparam>
		/// <param name="document">The document to update.</param>
		/// <returns>
		/// The asynchronous operation that returns the instance of <see cref="TDocument" />..
		/// </returns>
		public async Task<TDocument> UpdateAsync<TDocument>(TDocument document)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = await this.GetCollectionAsync<TDocument>();

			return await collection.FindOneAndReplaceAsync(data => data.ID == document.ID, document);
		}

		/// <summary>
		/// <see cref="Upserts"/> the existing document asynchronously or creates a new one if no documents matched.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document to <see cref="upsert"/>.</typeparam>
		/// <param name="document">The document to <see cref="upsert"/>.</param>
		/// <returns>
		/// The asynchronous operation that returns the instance of <see cref="TDocument" />..
		/// </returns>
		public async Task<TDocument> UpsertAsync<TDocument>(TDocument document)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = await this.GetCollectionAsync<TDocument>();

			return await collection.FindOneAndReplaceAsync<TDocument>(
				data => data.ID == document.ID,
				document,
				new FindOneAndReplaceOptions<TDocument, TDocument>()
				{
					IsUpsert = true,
					ReturnDocument = ReturnDocument.After
				});
		}

		/// <summary>
		/// Looks for the first document in the result set by specified document selector asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <param name="selector">The selector.</param>
		/// <returns>The asynchronous operation that returns the instance of <see cref="TDocument" />.</returns>
		public async Task<TDocument> GetAsync<TDocument>(Expression<Func<TDocument, bool>> selector)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = await this.GetCollectionAsync<TDocument>();

			return await collection.Find(selector).FirstOrDefaultAsync();
		}

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
		public async Task<IEnumerable<TDocument>> FindAsync<TDocument>(Expression<Func<TDocument, bool>> selector, int? offset = null, int? limit = null)
			where TDocument : IDocument
		{
			IMongoCollection<TDocument> collection = await this.GetCollectionAsync<TDocument>();

			return await collection.Find(selector).Skip(offset).Limit(limit).ToListAsync();
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
			IMongoCollection<TDocument> collection = await this.GetCollectionAsync<TDocument>();

			await collection.FindOneAndDeleteAsync(selector);
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the database collection.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document.</typeparam>
		/// <returns>The asynchronous operation resulting instance of <see cref="IMongoCollection{TDocument}"/>.</returns>
		private async Task<IMongoCollection<TDocument>> GetCollectionAsync<TDocument>()
			where TDocument : IDocument
		{
			Type type = typeof(TDocument);

			DocumentCollectionAttribute collectionAttribute = (type.GetCustomAttributes(typeof(DocumentCollectionAttribute), false)?.FirstOrDefault() as DocumentCollectionAttribute);
			string collectionName = collectionAttribute?.CollectionName ?? typeof(TDocument).Name;

			IAsyncCursor<BsonDocument> collections = await database.ListCollectionsAsync(
																						new ListCollectionsOptions
																						{
																							Filter = new BsonDocument("name", collectionName)
																						});

			bool isExists = await collections.AnyAsync();

			if (!isExists)
			{
				await database.CreateCollectionAsync(collectionName);
			}

			return database.GetCollection<TDocument>(collectionName);
		}

		#endregion
	}
}
