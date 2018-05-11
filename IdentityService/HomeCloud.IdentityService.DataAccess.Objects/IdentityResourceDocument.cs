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
	/// Represents <see cref="identity"/> resource document.
	/// </summary>
	[MongoDBCollection("identity_resource")]
	public class IdentityResourceDocument
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
		[BsonIgnoreIfDefault]
		[BsonRequired]
		public string ID { get; set; }

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
		/// Gets or sets the list of claims.
		/// </summary>
		/// <value>
		/// The claims.
		/// </value>
		[BsonElement("claims")]
		public IEnumerable<string> Claims { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the user can de-select the scope on the consent screen (if the consent screen wants to implement such a feature). Defaults to false.
		/// </summary>
		/// <value>
		/// <c>true</c> if required; otherwise, <c>false</c>.
		/// </value>
		[BsonElement("required")]
		public bool Required { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the consent screen will emphasize this scope (if the consent screen wants to implement such a feature). Use this setting for sensitive or important scopes. Defaults to false.
		/// </summary>
		/// <value>
		///   <c>true</c> if emphasize; otherwise, <c>false</c>.
		/// </value>
		[BsonElement("required")]
		public bool Emphasize { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this scope is shown in the discovery document. Defaults to true.
		/// </summary>
		/// <value>
		///   <c>true</c> if [show in discovery document]; otherwise, <c>false</c>.
		/// </value>
		[BsonElement("show_in_discovery_document")]
		public bool ShowInDiscoveryDocument { get; set; }
	}
}
