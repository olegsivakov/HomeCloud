namespace HomeCloud.DataStorage.DataAccess.Aggregation.Objects
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;

	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Represents catalog document.
	/// </summary>
	[MongoDBCollection("catalogs")]
	public class CatalogDocument
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonId(IdGenerator = typeof(NullIdChecker))]
		[BsonIgnoreIfDefault]
		[BsonRequired]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		[BsonElement("path")]
		[BsonRequired]
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[BsonElement("size")]
		public long Size { get; set; }
	}
}
