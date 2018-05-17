namespace HomeCloud.IdentityService.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents <see cref="Grant"/> search criteria entity.
	/// </summary>
	public class GrantSearchCriteria
	{
		// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public Guid? UserID { get; set; }

		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		/// <value>
		/// The client identifier.
		/// </value>
		public Guid? ClientID { get; set; }
	}
}
