namespace HomeCloud.Data.MongoDB
{
	#region Usings

	using System.Threading.Tasks;

	using global::MongoDB.Driver;

	#endregion

	/// <summary>
	/// Defines the context to access to the data stored in <see cref="MongoDB"/> database.
	/// </summary>
	public interface IMongoDBContext
	{
		/// <summary>
		/// Gets the database collection asynchronously.
		/// </summary>
		/// <typeparam name="TDocument">The type of the document for the collection.</typeparam>
		/// <returns>The instance of <see cref="IMongoCollection{TDocument}"/>.</returns>
		Task<IMongoCollection<TDocument>> GetCollectionAsync<TDocument>();
	}
}
