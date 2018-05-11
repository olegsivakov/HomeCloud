namespace HomeCloud.IdentityService.Stores.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.IdentityService.DataAccess.Objects;

	using IdentityServer4.Models;

	#endregion

	/// <summary>
	/// Provides methods to convert instances of <see cref="IdentityServer4.Models.PersistedGrant" />.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.GrantDocument, IdentityServer4.Models.PersistedGrant}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{IdentityServer4.Models.PersistedGrant, HomeCloud.IdentityService.DataAccess.Objects.GrantDocument}" />
	public class PersistedGrantConverter : ITypeConverter<GrantDocument, PersistedGrant>, ITypeConverter<PersistedGrant, GrantDocument>
	{
		#region ITypeConverter<GrantDocument, PersistedGrant> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public PersistedGrant Convert(GrantDocument source, PersistedGrant target)
		{
			target.Key = source.ID;
			target.Type = source.Type;
			target.SubjectId = source.SubjectID;
			target.ClientId = source.ClientID;
			target.CreationTime = source.CreationTime;
			target.Expiration = source.Expiration;
			target.Data = source.Data;

			return target;
		}

		#endregion

		#region ITypeConverter<PersistedGrant, GrantDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public GrantDocument Convert(PersistedGrant source, GrantDocument target)
		{
			target.ID = source.Key;
			target.Type = source.Type;
			target.SubjectID = source.SubjectId;
			target.ClientID = source.ClientId;
			target.CreationTime = source.CreationTime;
			target.Expiration = source.Expiration;
			target.Data = source.Data;

			return target;
		}

		#endregion
	}
}
