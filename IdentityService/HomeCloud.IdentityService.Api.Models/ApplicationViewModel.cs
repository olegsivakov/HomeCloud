namespace HomeCloud.IdentityService.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents application view model.
	/// </summary>
	public class ApplicationViewModel
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
