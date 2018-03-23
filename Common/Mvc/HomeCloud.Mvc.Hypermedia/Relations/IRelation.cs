namespace HomeCloud.Mvc.Hypermedia.Relations
{
	#region Usings

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Defines <see cref="HATEOAS"/> relation entry.
	/// </summary>
	public interface IRelation
	{
		/// <summary>
		/// Gets or sets the relation.
		/// </summary>
		/// <value>
		/// The relation.
		/// </value>
		[JsonProperty("rel")]
		string Name { get; set; }
	}
}
