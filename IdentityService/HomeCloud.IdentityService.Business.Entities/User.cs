namespace HomeCloud.IdentityService.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the user entity.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>
		/// The username.
		/// </value>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		public string LastName { get; set; }

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value>
		/// The full name.
		/// </value>
		public string FullName => $"{this.FirstName ?? string.Empty}{(!string.IsNullOrWhiteSpace(this.FirstName) && !string.IsNullOrWhiteSpace(this.LastName) ? " " : string.Empty)}{this.LastName ?? string.Empty}";

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		public Role Role { get; set; }
	}
}
