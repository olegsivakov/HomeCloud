namespace HomeCloud.IdentityService.Api.Models
{
	/// <summary>
	/// Represents client application view model.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Api.Models.ApplicationViewModel" />
	public class ClientViewModel : ApplicationViewModel
	{
		/// <summary>
		/// Gets or sets the redirect URL.
		/// </summary>
		/// <value>
		/// The redirect URL.
		/// </value>
		public string RedirectUrl { get; set; }

		/// <summary>
		/// Gets or sets the post logout redirect URL.
		/// </summary>
		/// <value>
		/// The post logout redirect URL.
		/// </value>
		public string PostLogoutRedirectUrl { get; set; }

		/// <summary>
		/// Gets or sets the identity token lifetime.
		/// </summary>
		/// <value>
		/// The identity token lifetime.
		/// </value>
		public int? IdentityTokenLifetime { get; set; }

		/// <summary>
		/// Gets or sets the access token lifetime.
		/// </summary>
		/// <value>
		/// The access token lifetime.
		/// </value>
		public int? AccessTokenLifetime { get; set; }

		/// <summary>
		/// Gets or sets the absolute refresh token lifetime.
		/// </summary>
		/// <value>
		/// The absolute refresh token lifetime.
		/// </value>
		public int? AbsoluteRefreshTokenLifetime { get; set; }

		/// <summary>
		/// Gets or sets the sliding refresh token lifetime.
		/// </summary>
		/// <value>
		/// The sliding refresh token lifetime.
		/// </value>
		public int? SlidingRefreshTokenLifetime { get; set; }
	}
}
