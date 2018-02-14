namespace HomeCloud.Mvc.Models
{
	#region Usings

	using HomeCloud.Http;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the response model containing <see cref="LocationUrl"/> header.
	/// </summary>
	public class LocationModel
	{
		/// <summary>
		/// Gets or sets the location <see cref="URL"/> value.
		/// </summary>
		/// <value>
		/// The location <see cref="URL"/>.
		/// </value>
		[HttpHeader("Location", HttpMethods.Post, HttpMethods.Put)]
		[JsonIgnore]
		public string LocationUrl { get; set; }
	}
}
