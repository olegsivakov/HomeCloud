namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Defines methods to handle links.
	/// </summary>
	public interface ILinkService
	{
		/// <summary>
		/// Gets the collection of links for the specified model instance within the specified route.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="route">The name of the route.</param>
		/// <returns>the list of <see cref="Link"/>.</returns>
		IEnumerable<Link> GetLinks(object model, string route);
	}
}
