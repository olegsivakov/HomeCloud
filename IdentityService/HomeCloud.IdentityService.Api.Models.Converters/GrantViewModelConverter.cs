namespace HomeCloud.IdentityService.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities;

	#endregion

	/// <summary>
	/// Provides convertion methods for <see cref="GrantViewModel" /> entity.
	/// </summary>
	public class GrantViewModelConverter : ITypeConverter<Grant, GrantViewModel>, ITypeConverter<GrantViewModel, Grant>
	{
		#region ITypeConverter<Grant, GrantViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public GrantViewModel Convert(Grant source, GrantViewModel target)
		{
			target.ID = source.ID;
			target.Type = source.Type;
			target.ClientID = source.ClientID;
			target.UserID = source.UserID;
			target.CreationTime = source.CreationTime;
			target.ExpirationTime = source.Expiration;
			target.Data = source.Data;

			return target;
		}

		#endregion

		#region ITypeConverter<GrantViewModel, Grant> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Grant Convert(GrantViewModel source, Grant target)
		{
			target.ID = source.ID;
			target.Type = source.Type;
			target.ClientID = source.ClientID;
			target.UserID = source.UserID;
			target.CreationTime = source.CreationTime;
			target.ExpirationTime = source.Expiration;
			target.Data = source.Data;

			return target;
		}

		#endregion
	}
}
