namespace HomeCloud.IdentityService.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities.Applications;

	#endregion

	/// <summary>
	/// Provides convertion methods for <see cref="ApiResourceViewModel" /> entity.
	/// </summary>
	public class ApiResourceViewModelConverter : ApplicationViewModelConverter, ITypeConverter<ApiResource, ApiResourceViewModel>, ITypeConverter<ApiResourceViewModel, ApiResource>
	{
		#region ITypeConverter<ApiResource, ApiResourceViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApiResourceViewModel Convert(ApiResource source, ApiResourceViewModel target)
		{
			target = (ApiResourceViewModel)base.Convert(source, target);

			return target;
		}

		#endregion

		#region ITypeConverter<ApiResourceViewModel, ApiResource> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApiResource Convert(ApiResourceViewModel source, ApiResource target)
		{
			target = (ApiResource)base.Convert(source, target);

			return target;
		}

		#endregion
	}
}
