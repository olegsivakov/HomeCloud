namespace HomeCloud.IdentityService.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities.Applications;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="ApiResource" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument, HomeCloud.IdentityService.Business.Entities.Applications.ApiResource}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Applications.ApiResource, HomeCloud.IdentityService.DataAccess.Objects.ApiResourceDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Applications.ApiResource, HomeCloud.IdentityService.Business.Entities.Applications.ApiResource}" />
	public class ApiResourceConverter : ITypeConverter<ApiResourceDocument, ApiResource>, ITypeConverter<ApiResource, ApiResourceDocument>, ITypeConverter<ApiResource, ApiResource>
	{
		#region ITypeConverter<ApiResourceDocument, ApiResource> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApiResource Convert(ApiResourceDocument source, ApiResource target)
		{
			target.ID = source.ID;
			target.Name = source.Name;

			return target;
		}

		#endregion

		#region ITypeConverter<ApiResource, ApiResourceDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApiResourceDocument Convert(ApiResource source, ApiResourceDocument target)
		{
			target.ID = source.ID;
			target.Name = source.Name;

			return target;
		}

		#endregion

		#region ITypeConverter<ApiResource, ApiResource> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public ApiResource Convert(ApiResource source, ApiResource target)
		{
			target.ID = target.ID == Guid.Empty ? source.ID : target.ID;
			target.Name = target.Name is null ? source.Name : target.Name;

			return target;
		}

		#endregion
	}
}
