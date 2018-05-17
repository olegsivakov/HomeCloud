namespace HomeCloud.IdentityService.Business.Entities.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="Secret" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.SecretDocument, HomeCloud.IdentityService.Business.Entities.Applications.Secret}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Applications.Secret, HomeCloud.IdentityService.DataAccess.Objects.SecretDocument}" />
	public class SecretConverter : ITypeConverter<SecretDocument, Secret>, ITypeConverter<Secret, SecretDocument>
	{
		#region ITypeConverter<SecretDocument, Secret> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Secret Convert(SecretDocument source, Secret target)
		{
			target.Value = source.Value;
			target.Expiration = source.Expiration;

			return target;
		}

		#endregion

		#region ITypeConverter<Secret, SecretDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public SecretDocument Convert(Secret source, SecretDocument target)
		{
			target.Value = source.Value;
			target.Expiration = source.Expiration;

			return target;
		}

		#endregion
	}
}
