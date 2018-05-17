namespace HomeCloud.IdentityService.DataAccess.Objects
{
	#region Usings

	using System;

	using MongoDB.Bson.Serialization.Attributes;

	#endregion

	/// <summary>
	/// Represents secret key document.
	/// </summary>
	public class SecretDocument
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		[BsonElement("value")]
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the expiration time.
		/// </summary>
		/// <value>
		/// The expiration time.
		/// </value>
		[BsonElement("value")]
		public DateTime? Expiration { get; set; }
	}
}
