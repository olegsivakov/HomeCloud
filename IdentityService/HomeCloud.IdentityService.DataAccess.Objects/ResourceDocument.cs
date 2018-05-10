namespace HomeCloud.IdentityService.DataAccess.Objects
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.Data.MongoDB;

	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Represents <see cref="api"/> resource application document.
	/// </summary>
	[MongoDBCollection("clients")]
	public class ResourceDocument
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonId(IdGenerator = typeof(NullIdChecker))]
		[BsonIgnoreIfDefault]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		[BsonElement("name")]
		[BsonRequired]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the secrets.
		/// </summary>
		/// <value>
		/// The secrets.
		/// </value>
		[BsonElement("secrets")]
		[BsonRequired]
		public IEnumerable<string> Secrets { get; set; }

		/// <summary>
		/// Gets or sets the scopes.
		/// </summary>
		/// <value>
		/// The scopes.
		/// </value>
		[BsonElement("scopes")]
		[BsonRequired]
		public IEnumerable<string> Scopes { get; set; }
	}
}
