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
	/// Represents client application document.
	/// </summary>
	[MongoDBCollection("clients")]
	public class ClientDocument
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
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		[BsonRequired]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the grant type enumeration value.
		/// </summary>
		/// <value>
		/// The grant type enumeration value.
		/// </value>
		[BsonElement("grant_type")]
		[BsonRequired]
		public int GrantType { get; set; }

		/// <summary>
		/// Gets or sets the scopes.
		/// </summary>
		/// <value>
		/// The scopes.
		/// </value>
		[BsonElement("scopes")]
		[BsonIgnoreIfDefault]
		public IEnumerable<string> Scopes { get; set; }

		/// <summary>
		/// Gets or sets the secrets.
		/// </summary>
		/// <value>
		/// The secrets.
		/// </value>
		[BsonElement("secrets")]
		[BsonIgnoreIfDefault]
		public IEnumerable<SecretDocument> Secrets { get; set; }

		/// <summary>
		/// Gets or sets the origins.
		/// </summary>
		/// <value>
		/// The origins.
		/// </value>
		[BsonElement("origins")]
		[BsonIgnoreIfDefault]
		public IEnumerable<string> Origins { get; set; }

		/// <summary>
		/// Gets or sets the grants.
		/// </summary>
		/// <value>
		/// The grants.
		/// </value>
		[BsonElement("grants")]
		[BsonIgnoreIfDefault]
		public IEnumerable<GrantDocument> Grants { get; set; }

		/// <summary>
		/// Gets or sets the redirect uris.
		/// </summary>
		/// <value>
		/// The redirect uris.
		/// </value>
		[BsonElement("redirect_url")]
		public string RedirectUrl { get; set; }

		/// <summary>
		/// Gets or sets the post logout redirect uris.
		/// </summary>
		/// <value>
		/// The post logout redirect uris.
		/// </value>
		[BsonElement("post_logout_redirect_url")]
		public string PostLogoutRedirectUrl { get; set; }

		/// <summary>
		/// Gets or sets the identity token lifetime.
		/// </summary>
		/// <value>
		/// The identity token lifetime.
		/// </value>
		[BsonElement("identity_token_lifetime")]
		public int? IdentityTokenLifetime { get; set; }

		/// <summary>
		/// Gets or sets the access token lifetime.
		/// </summary>
		/// <value>
		/// The access token lifetime.
		/// </value>
		[BsonElement("access_token_lifetime")]
		public int? AccessTokenLifetime { get; set; }

		/// <summary>
		/// Gets or sets the absolute refresh token lifetime.
		/// </summary>
		/// <value>
		/// The absolute refresh token lifetime.
		/// </value>
		[BsonElement("absolute_refresh_token_lifetime")]
		public int? AbsoluteRefreshTokenLifetime { get; set; }

		/// <summary>
		/// Gets or sets the sliding refresh token lifetime.
		/// </summary>
		/// <value>
		/// The sliding refresh token lifetime.
		/// </value>
		[BsonElement("sliding_refresh_token_lifetime")]
		public int? SlidingRefreshTokenLifetime { get; set; }
	}
}
