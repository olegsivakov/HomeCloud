namespace HomeCloud.Mvc.Hypermedia.Relations
{
	#region Usings

	using System.Collections.Generic;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents single <see cref="HATEOAS" /> relation entry.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.Hypermedia.Relations.IRelation" />
	internal class RelationList : IRelation
	{
		/// <summary>
		/// Gets or sets the relation.
		/// </summary>
		/// <value>
		/// The relation.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the hypermedia links collection.
		/// </summary>
		/// <value>
		/// The hypermedia link collection.
		/// </value>
		[JsonProperty("items")]
		public IEnumerable<Link> Links { get; set; }
	}
}
