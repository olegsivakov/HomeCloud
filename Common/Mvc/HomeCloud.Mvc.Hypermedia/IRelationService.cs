namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System.Collections.Generic;
	using HomeCloud.Mvc.Hypermedia.Relations;

	#endregion

	/// <summary>
	/// Defines methods to handle relations.
	/// </summary>
	public interface IRelationService
	{
		/// <summary>
		/// Gets the collection of relations for the specified model instance within the specified route.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="route">The name of the route.</param>
		/// <returns>the list of <see cref="IRelation"/>.</returns>
		IEnumerable<IRelation> GetRelations(object model, string route);
	}
}
