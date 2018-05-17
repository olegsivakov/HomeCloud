namespace HomeCloud.IdentityService.DataAccess.Objects
{
	#region Usings

	using System;

	using MongoDB.Bson.Serialization.Attributes;

	#endregion

	/// <summary>
	/// Represents grant document.
	/// </summary>
	public class GrantDocument
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonElement("id")]
		[BsonRequired]
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[BsonElement("type")]
		[BsonRequired]
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		[BsonElement("userId")]
		[BsonIgnoreIfDefault]
		public Guid UserID { get; set; }

		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		/// <value>
		/// The client identifier.
		/// </value>
		[BsonIgnore]
		[BsonRequired]
		public Guid ClientID { get; set; }

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>
		/// The creation time.
		/// </value>
		[BsonElement("creation_time")]
		[BsonRequired]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the expiration time.
		/// </summary>
		/// <value>
		/// The expiration time.
		/// </value>
		[BsonElement("expiration_time")]
		public DateTime? Expiration { get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		[BsonElement("data")]
		[BsonRequired]
		public string Data { get; set; }
	}
}
