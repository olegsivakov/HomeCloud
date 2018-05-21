namespace HomeCloud.IdentityService.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents secret view model.
	/// </summary>
	public class SecretViewModel
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the expiration time.
		/// </summary>
		/// <value>
		/// The expiration time.
		/// </value>
		public DateTime? Expiration { get; set; }
	}
}
