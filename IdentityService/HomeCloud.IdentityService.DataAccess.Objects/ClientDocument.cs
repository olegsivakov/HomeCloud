using System.Collections.Generic;

namespace HomeCloud.IdentityService.DataAccess.Objects
{
	/// <summary>
	/// Represents client application document.
	/// </summary>
	public class ClientDocument
	{
		public string ID { get; set; }

		public string Name { get; set; }

		public IEnumerable<string> GrantTypes { get; set; }

		public IEnumerable<string> RedirectUris { get; set; }

		public IEnumerable<string> PostLogoutRedirectUris { get; set; }

		public IEnumerable<string> Origins { get; set; }

		public IEnumerable<string> Scopes { get; set; }

		public int IdentityTokenLifetime { get; set; }

		public int AccessTokenLifetime { get; set; }

		public int AbsoluteRefreshTokenLifetime { get; set; }

		public int SlidingRefreshTokenLifetime { get; set; }
	}
}
