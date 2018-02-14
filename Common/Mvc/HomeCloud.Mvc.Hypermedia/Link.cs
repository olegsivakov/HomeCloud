namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the hypermedia link.
	/// </summary>
	public class Link
	{
		/// <summary>
		/// Gets or sets the relation.
		/// </summary>
		/// <value>
		/// The relation.
		/// </value>
		[JsonProperty("rel")]
		public string Relation { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="URL"/>.
		/// </summary>
		/// <value>
		/// The <see cref="URL"/>.
		/// </value>
		[JsonProperty("href")]
		public string Href { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="HTTP"/> method for <see cref="Link.Href"/>.
		/// </summary>
		/// <value>
		/// The <see cref="HTTP"/> method for <see cref="Link.Href"/>.
		/// </value>
		[JsonProperty("method")]
		public string Method { get; set; }
	}
}
