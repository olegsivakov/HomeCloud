namespace HomeCloud.IdentityService.Stores.Converters
{
	#region Usings

	using System.Linq;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using IdentityServer4.Models;

	#endregion

	/// <summary>
	/// Provides methods to convert instances of <see cref="IdentityServer4.Models.Client"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.ClientDocument, IdentityServer4.Models.Client}" />
	public sealed class ClientConverter : ITypeConverter<ClientDocument, Client>
	{
		#region ITypeConverter<ClientDocument, Client> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Client Convert(ClientDocument source, Client target)
		{
			target.ClientId = source.ID.ToString();
			target.ClientName = source.Name;
			target.AllowedGrantTypes = source.GrantTypes?.ToList() ?? target.AllowedGrantTypes;
			target.AllowedScopes = source.Scopes?.ToList() ?? target.AllowedScopes;
			target.ClientSecrets = source.Secrets?.Select(secret => new Secret(secret))?.ToList() ?? target.ClientSecrets;
			target.AllowedCorsOrigins = source.Origins?.ToList() ?? target.AllowedCorsOrigins;
			target.RedirectUris =  source.RedirectUris?.ToList() ?? target.RedirectUris;
			target.PostLogoutRedirectUris = source.PostLogoutRedirectUris?.ToList() ?? target.PostLogoutRedirectUris;
			target.IdentityTokenLifetime = source.IdentityTokenLifetime.HasValue ? source.IdentityTokenLifetime.Value : target.IdentityTokenLifetime;
			target.AccessTokenLifetime = source.AccessTokenLifetime.HasValue ? source.AccessTokenLifetime.Value : target.AccessTokenLifetime;
			target.AbsoluteRefreshTokenLifetime = source.AbsoluteRefreshTokenLifetime.HasValue ? source.AbsoluteRefreshTokenLifetime.Value : target.AbsoluteRefreshTokenLifetime;
			target.SlidingRefreshTokenLifetime = source.SlidingRefreshTokenLifetime.HasValue ? source.SlidingRefreshTokenLifetime.Value : target.SlidingRefreshTokenLifetime;

			return target;
		}

		#endregion
	}
}
