namespace HomeCloud.IdentityService.Business.Entities.Applications
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents application.
	/// </summary>
	public class Application
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }
	}
}
