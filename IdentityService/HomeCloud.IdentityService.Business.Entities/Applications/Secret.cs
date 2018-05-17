namespace HomeCloud.IdentityService.Business.Entities.Applications
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents application secret entity.
	/// </summary>
	public class Secret
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the expiration.
		/// </summary>
		/// <value>
		/// The expiration.
		/// </value>
		public DateTime? Expiration { get; set; }
	}
}
