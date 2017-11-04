namespace HomeCloud.DataStorage.Business.Entities.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="ServiceResult" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.Validation.ValidationResult, HomeCloud.DataStorage.Business.Entities.ServiceResult}" />
	public class ServiceResultConverter : ITypeConverter<ValidationResult, ServiceResult>
	{
		#region  ITypeConverter<ValidationResult, ServiceResult> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ServiceResult Convert(ValidationResult source, ServiceResult target)
		{
			target.Errors = target.Errors;

			return target;
		}

		#endregion
	}
}
