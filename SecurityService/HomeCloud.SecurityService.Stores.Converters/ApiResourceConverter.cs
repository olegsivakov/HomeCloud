namespace HomeCloud.IdentityService.Stores.Converters
{
	#region Usings

	using System.Linq;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using IdentityServer4.Models;

	#endregion

	/// <summary>
	/// Provides methods to convert instances of <see cref="IdentityServer4.Models.ApiResource"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument, IdentityServer4.Models.ApiResource}" />
	public sealed class ApiResourceConverter : ITypeConverter<ApiResourceDocument, ApiResource>
	{
		#region ITypeConverter<ApiResourceDocument, ApiResource> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApiResource Convert(ApiResourceDocument source, ApiResource target)
		{
			target.Name = source.ID.ToString();
			target.DisplayName = source.Name;
			target.UserClaims = source.Claims?.ToList() ?? target.UserClaims;
			target.Scopes = source.Scopes?.Select(scope => new Scope(scope)).ToList() ?? target.Scopes;
			target.ApiSecrets = source.Secrets?.Select(secret => new Secret(secret))?.ToList() ?? target.ApiSecrets;

			return target;
		}

		#endregion
	}
}
