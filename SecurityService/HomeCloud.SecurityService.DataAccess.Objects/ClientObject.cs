namespace HomeCloud.SecurityService.DataAccess.Objects
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents client application response object.
	/// </summary>
	public class ClientObject
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

		/// <summary>
		/// Gets or sets the grant types.
		/// </summary>
		/// <value>
		/// The grant types.
		/// </value>
		public IEnumerable<string> GrantTypes { get; set; }

		/// <summary>
		/// Gets or sets the scopes.
		/// </summary>
		/// <value>
		/// The scopes.
		/// </value>
		public IEnumerable<string> Scopes { get; set; }

		/// <summary>
		/// Gets or sets the secrets.
		/// </summary>
		/// <value>
		/// The secrets.
		/// </value>
		public IEnumerable<string> Secrets { get; set; }

		/// <summary>
		/// Gets or sets the origins.
		/// </summary>
		/// <value>
		/// The origins.
		/// </value>
		public IEnumerable<string> Origins { get; set; }

		/// <summary>
		/// Gets or sets the redirect uris.
		/// </summary>
		/// <value>
		/// The redirect uris.
		/// </value>
		public IEnumerable<string> RedirectUris { get; set; }

		/// <summary>
		/// Gets or sets the post logout redirect uris.
		/// </summary>
		/// <value>
		/// The post logout redirect uris.
		/// </value>
		public IEnumerable<string> PostLogoutRedirectUris { get; set; }

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
