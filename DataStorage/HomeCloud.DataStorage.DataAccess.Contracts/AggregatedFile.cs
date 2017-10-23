namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;

	using HomeCloud.DataAccess.Contracts;

	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Represents aggregated catalog document.
	/// </summary>
	public class AggregatedFile : IDocument
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonId(IdGenerator = typeof(NullIdChecker))]
		[BsonRequired]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		[BsonElement]
		[BsonRequired]
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[BsonElement]
		public long Size { get; set; }
	}
}
