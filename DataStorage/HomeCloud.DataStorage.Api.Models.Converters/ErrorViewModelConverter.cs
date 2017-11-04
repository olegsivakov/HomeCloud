namespace HomeCloud.DataStorage.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Api.Mvc;
	using HomeCloud.Core;
	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="ErrorViewModel" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.DataStorage.Business.Entities.ServiceResult, HomeCloud.Api.Mvc.ErrorViewModel}" />
	public class ErrorViewModelConverter : ITypeConverter<ServiceResult, ErrorViewModel>
	{
		#region ITypeConverter<ServiceResult, ErrorViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ErrorViewModel Convert(ServiceResult source, ErrorViewModel target)
		{
			target.Errors = source.Errors;

			return target;
		}

		#endregion
	}
}
