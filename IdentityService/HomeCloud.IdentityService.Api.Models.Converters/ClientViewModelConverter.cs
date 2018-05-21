namespace HomeCloud.IdentityService.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities;
	using HomeCloud.IdentityService.Business.Entities.Applications;

	#endregion

	/// <summary>
	/// Provides convertion methods for <see cref="ClientViewModel" /> entity.
	/// </summary>
	public class ClientViewModelConverter : ApplicationViewModelConverter, ITypeConverter<Client, ClientViewModel>, ITypeConverter<ClientViewModel, Client>
	{
		#region ITypeConverter<Client, ClientViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ClientViewModel Convert(Client source, ClientViewModel target)
		{
			target = (ClientViewModel)base.Convert(source, target);

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

		#region ITypeConverter<ClientViewModel, Client> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Client Convert(ClientViewModel source, Client target)
		{
			target = (Client)base.Convert(source, target);

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
	}
}
