namespace HomeCloud.Mvc.Hypermedia.Relations
{
	#region Usings

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents single <see cref="HATEOAS" /> relation entry.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Hypermedia.Relations.IRelation" />
	internal class Relation : IRelation
	{
		/// <summary>
		/// Gets or sets the relation.
		/// </summary>
		/// <value>
		/// The relation.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the hypermedia link.
		/// </summary>
		/// <value>
		/// The hypermedia link.
		/// </value>
		[JsonIgnore]
		public Link Link { get; set; }
	}
}
