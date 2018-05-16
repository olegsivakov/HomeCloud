namespace HomeCloud.IdentityService.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents grant entity.
	/// </summary>
	public class Grant
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
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public Guid UserID { get; set; }

		/// <summary>
		/// Gets or sets the client application identifier.
		/// </summary>
		/// <value>
		/// The client application identifier.
		/// </value>
		public Guid ClientID { get; set; }

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
		public DateTime? Expiration { get; set; }

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public string Data { get; set; }
	}
}
