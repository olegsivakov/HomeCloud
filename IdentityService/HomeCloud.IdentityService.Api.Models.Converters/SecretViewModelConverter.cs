namespace HomeCloud.IdentityService.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities.Applications;

	#endregion

	/// <summary>
	/// Provides convertion methods for <see cref="SecretViewModel" /> entity.
	/// </summary>
	public class SecretViewModelConverter : ITypeConverter<Secret, SecretViewModel>, ITypeConverter<SecretViewModel, Secret>
	{
		#region ITypeConverter<Secret, SecretViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public SecretViewModel Convert(Secret source, SecretViewModel target)
		{
			target.Value = source.Value;
			target.Expiration = source.Expiration;

			return target;
		}

		#endregion

		#region ITypeConverter<SecretViewModel, Secret> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Secret Convert(SecretViewModel source, Secret target)
		{

			target.Value = source.Value;
			target.Expiration = source.Expiration;

			return target;
		}

		#endregion
	}
}
