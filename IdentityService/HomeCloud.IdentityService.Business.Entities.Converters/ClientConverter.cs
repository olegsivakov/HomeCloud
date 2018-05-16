namespace HomeCloud.IdentityService.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Client" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Applications.Client, HomeCloud.IdentityService.Business.Entities.Applications.Client}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.ClientDocument, HomeCloud.IdentityService.Business.Entities.Applications.Client}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Applications.Client, HomeCloud.IdentityService.DataAccess.Objects.ClientDocument}" />
	public class ClientConverter : ITypeConverter<ClientDocument, Client>, ITypeConverter<Client, ClientDocument>, ITypeConverter<Client, Client>
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
			target.ID = source.ID;
			target.Name = source.Name;
			target.GrantType = (GrantType)source.GrantType;
			target.RedirectUrl = source.RedirectUrl;
			target.PostLogoutRedirectUrl = source.PostLogoutRedirectUrl;
			target.IdentityTokenLifetime = source.IdentityTokenLifetime;
			target.AccessTokenLifetime = source.AccessTokenLifetime;
			target.AbsoluteRefreshTokenLifetime = source.AbsoluteRefreshTokenLifetime;
			target.SlidingRefreshTokenLifetime = source.SlidingRefreshTokenLifetime;

			return target;
		}

		#endregion

		#region ITypeConverter<Client, ClientDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ClientDocument Convert(Client source, ClientDocument target)
		{
			target.ID = source.ID;
			target.Name = source.Name;
			target.GrantType = (int)source.GrantType;
			target.RedirectUrl = source.RedirectUrl;
			target.PostLogoutRedirectUrl = source.PostLogoutRedirectUrl;
			target.IdentityTokenLifetime = source.IdentityTokenLifetime;
			target.AccessTokenLifetime = source.AccessTokenLifetime;
			target.AbsoluteRefreshTokenLifetime = source.AbsoluteRefreshTokenLifetime;
			target.SlidingRefreshTokenLifetime = source.SlidingRefreshTokenLifetime;

			return target;
		}

		#endregion

		#region ITypeConverter<Client, Client> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Client Convert(Client source, Client target)
		{
			target.ID = target.ID == Guid.Empty ? source.ID : target.ID;
			target.Name = target.Name is null ? source.Name : target.Name;
			target.GrantType = target.GrantType == GrantType.Unknown ? source.GrantType : target.GrantType;

			target.RedirectUrl = target.RedirectUrl is null ? source.RedirectUrl : target.RedirectUrl;
			target.PostLogoutRedirectUrl = target.PostLogoutRedirectUrl is null ? source.PostLogoutRedirectUrl : target.PostLogoutRedirectUrl;
			target.IdentityTokenLifetime = !target.IdentityTokenLifetime.HasValue ? source.IdentityTokenLifetime : target.IdentityTokenLifetime;
			target.AccessTokenLifetime = !target.AccessTokenLifetime.HasValue ? source.AccessTokenLifetime : target.AccessTokenLifetime;
			target.AbsoluteRefreshTokenLifetime = !target.AbsoluteRefreshTokenLifetime.HasValue ? source.AbsoluteRefreshTokenLifetime : target.AbsoluteRefreshTokenLifetime;
			target.SlidingRefreshTokenLifetime = !target.SlidingRefreshTokenLifetime.HasValue ? source.SlidingRefreshTokenLifetime : target.SlidingRefreshTokenLifetime;

			return target;
		}

		#endregion
	}
}
