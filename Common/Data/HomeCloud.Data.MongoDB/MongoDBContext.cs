namespace HomeCloud.Data.MongoDB
{
	#region Usings

	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using global::MongoDB.Driver;
	using global::MongoDB.Bson;

	#endregion

	/// <summary>
	/// Provides the context to access to the data stored in <see cref="MongoDB"/> database.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.MongoDB.IMongoDBContext" />
	public class MongoDBContext : IMongoDBContext
	{
		#region Private Members

		/// <summary>
		/// The database
		/// </summary>
		private readonly IMongoDatabase _database = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MongoDBContext" /> class.
		/// </summary>
		/// <param name="options">The configuration options.</param>
		/// <exception cref="System.ArgumentNullException">connectionString</exception>
		public MongoDBContext(MongoDBOptions options)
		{
			if (options is null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			if (options.ConnectionString is null)
			{
				throw new ArgumentNullException(nameof(options.ConnectionString));
			}

			this._database = this._database ?? new MongoClient(options.ConnectionString).GetDatabase(MongoUrl.Create(options.ConnectionString).DatabaseName);
		}

		#endregion

		#region IMongoDBContext Implementations

		/// <summary>
		/// Gets the database collection asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document for the collection.</typeparam>
		/// <returns>The instance of <see cref="IMongoCollection{TDocument}"/>.</returns>
		public async Task<IMongoCollection<TDocument>> GetCollectionAsync<TDocument>()
		{
			Type type = typeof(TDocument);

			MongoDBCollectionAttribute collectionAttribute = (type.GetCustomAttributes(typeof(MongoDBCollectionAttribute), false)?.FirstOrDefault() as MongoDBCollectionAttribute);
			string collectionName = collectionAttribute?.CollectionName ?? typeof(TDocument).Name;

			IAsyncCursor<BsonDocument> collections = await this._database.ListCollectionsAsync(
																						new ListCollectionsOptions
																						{
																							Filter = new BsonDocument("name", collectionName)
																						});

			bool isExists = await collections.AnyAsync();
			if (!isExists)
			{
				await this._database.CreateCollectionAsync(collectionName);
			}

			return this._database.GetCollection<TDocument>(collectionName);
		}

		#endregion
	}
}
