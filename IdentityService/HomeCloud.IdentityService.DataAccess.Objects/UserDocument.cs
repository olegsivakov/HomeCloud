namespace HomeCloud.IdentityService.DataAccess.Objects
{
	#region Usings

	using System;

	using HomeCloud.Data.MongoDB;
	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Represents user document
	/// </summary>
	[MongoDBCollection("users")]
	public class UserDocument
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
		/// Gets or sets the username.
		/// </summary>
		/// <value>
		/// The username.
		/// </value>
		[BsonElement("username")]
		[BsonRequired]
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		[BsonElement("password")]
		[BsonRequired]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		[BsonElement("first_name")]
		[BsonRequired]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		[BsonElement("last_name")]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		[BsonElement("role")]
		public int Role { get; set; }
	}
}
