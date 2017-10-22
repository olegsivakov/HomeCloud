namespace HomeCloud.DataAccess.Contracts
{
	#region Usings

	using System;

	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Defines contract to be a document.
	/// </summary>
	public interface IDocument
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonId(IdGenerator = typeof(GuidGenerator))]
		Guid ID { get; set; }
	}
}
