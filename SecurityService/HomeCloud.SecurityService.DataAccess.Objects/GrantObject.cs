namespace HomeCloud.SecurityService.DataAccess.Objects
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents grant response object.
	/// </summary>
	public class GrantObject
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
		/// Gets or sets the subject identifier.
		/// </summary>
		/// <value>
		/// The subject identifier.
		/// </value>
		public string SubjectID { get; set; }

		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		/// <value>
		/// The client identifier.
		/// </value>
		public string ClientID { get; set; }

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
