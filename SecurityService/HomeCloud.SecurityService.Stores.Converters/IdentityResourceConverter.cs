namespace HomeCloud.IdentityService.Stores.Converters
{
	#region Usings

	using System.Linq;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using IdentityServer4.Models;

	#endregion

	/// <summary>
	/// Provides methods to convert instances of <see cref="IdentityServer4.Models.IdentityResource"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.IdentityResourceDocument, IdentityServer4.Models.IdentityResource}" />
	public class IdentityResourceConverter : ITypeConverter<IdentityResourceDocument, IdentityResource>
	{
		#region ITypeConverter<IdentityResourceDocument, IdentityResource> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public IdentityResource Convert(IdentityResourceDocument source, IdentityResource target)
		{
			target.Name = source.ID;
			target.DisplayName = source.Name;
			target.Emphasize = source.Emphasize;
			target.Required = source.Required;
			target.ShowInDiscoveryDocument = source.ShowInDiscoveryDocument;
			target.UserClaims = source.Claims?.ToList() ?? target.UserClaims;

			return target;
		}

		#endregion
	}
}
