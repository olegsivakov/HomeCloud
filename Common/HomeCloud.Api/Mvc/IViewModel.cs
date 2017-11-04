namespace HomeCloud.Api.Mvc
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines the <see cref="RESTful API"/> view model.
	/// </summary>
	public interface IViewModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		Guid ID { get; set; }
	}
}
