namespace HomeCloud.IdentityService.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents grant view model.
	/// </summary>
	public class GrantViewModel
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		/// <value>
		/// The client identifier.
		/// </value>
		public Guid ClientID { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public Guid? UserID { get; set; }

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>
		/// The creation time.
		/// </value>
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the expiration time.
		/// </summary>
		/// <value>
		/// The expiration time.
		/// </value>
		public DateTime? ExpirationTime { get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public string Data { get; set; }
	}
}
