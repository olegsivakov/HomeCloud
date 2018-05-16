namespace HomeCloud.IdentityService.Business.Entities.Applications
{
	/// <summary>
	/// Represents client application.
	/// </summary>
	/// <seealso cref="HomeCloud.IdentityService.Business.Entities.Applications.Application" />
	public class Client : Application
	{
		/// <summary>
		/// Gets or sets the type of the grant.
		/// </summary>
		/// <value>
		/// The type of the grant.
		/// </value>
		public GrantType GrantType { get; set; }

		/// <summary>
		/// Gets or sets the redirect uris.
		/// </summary>
		/// <value>
		/// The redirect uris.
		/// </value>
		public string RedirectUrl { get; set; }

		/// <summary>
		/// Gets or sets the post logout redirect uris.
		/// </summary>
		/// <value>
		/// The post logout redirect uris.
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
