namespace HomeCloud.IdentityService.Api.Models.Converters
{
	#region Usings

	using HomeCloud.Core;
	using HomeCloud.IdentityService.Business.Entities.Applications;

	#endregion

	/// <summary>
	/// Provides convertion methods for <see cref="ApplicationViewModel" /> entity.
	/// </summary>
	public class ApplicationViewModelConverter : ITypeConverter<Application, ApplicationViewModel>, ITypeConverter<ApplicationViewModel, Application>
	{
		#region ITypeConverter<Application, ApplicationViewModel> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApplicationViewModel Convert(Application source, ApplicationViewModel target)
		{
			target.ID = source.ID;
			target.Name = source.Name;

			return target;
		}

		#endregion

		#region ITypeConverter<ApplicationViewModel, Application> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public Application Convert(ApplicationViewModel source, Application target)
		{
			target.ID = source.ID;
			target.Name = source.Name;

			return target;
		}

		#endregion
	}
}
