namespace HomeCloud.IdentityService.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Grant" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.GrantDocument, HomeCloud.IdentityService.Business.Entities.Grant}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Grant, HomeCloud.IdentityService.DataAccess.Objects.GrantDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Grant, HomeCloud.IdentityService.Business.Entities.Grant}" />
	public class GrantConverter : ITypeConverter<GrantDocument, Grant>, ITypeConverter<Grant, GrantDocument>, ITypeConverter<Grant, Grant>
	{
		#region ITypeConverter<GrantDocument, Grant> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Grant Convert(GrantDocument source, Grant target)
		{
			target.ID = source.ID;
			target.Type = source.Type;
			target.ClientID = source.ClientID;
			target.UserID = source.UserID;
			target.CreationTime = source.CreationTime;
			target.Expiration = source.Expiration;
			target.Data = source.Data;

			return target;
		}

		#endregion

		#region ITypeConverter<Grant, GrantDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public GrantDocument Convert(Grant source, GrantDocument target)
		{
			target.ID = source.ID;
			target.Type = source.Type;
			target.ClientID = source.ClientID;
			target.UserID = source.UserID.GetValueOrDefault();
			target.CreationTime = source.CreationTime;
			target.Expiration = source.Expiration;
			target.Data = source.Data;

			return target;
		}

		#endregion

		#region ITypeConverter<Grant, Grant> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Grant Convert(Grant source, Grant target)
		{
			target.ID = target.ID is null ? source.ID : target.ID;
			target.Type = target.Type is null ? source.Type : target.Type;
			target.ClientID = target.ClientID == Guid.Empty ? source.ClientID : target.ClientID;
			target.UserID = target.UserID.GetValueOrDefault() == Guid.Empty ? source.UserID : target.UserID;
			target.CreationTime = target.CreationTime == DateTime.MinValue ? source.CreationTime : target.CreationTime;
			target.Expiration = !target.Expiration.HasValue ? source.Expiration : target.Expiration;
			target.Data = target.Data is null ? source.Data : target.Data;

			return target;
		}

		#endregion
	}
}
