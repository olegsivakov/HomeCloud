namespace HomeCloud.IdentityService.Business.Entities.Converters
{
	#region Usings

	using System;

	using HomeCloud.Core;

	using HomeCloud.IdentityService.Business.Entities.Membership;
	using HomeCloud.IdentityService.DataAccess.Objects;

	#endregion

	/// <summary>
	/// Provides converter methods for <see cref="User" /> entity.
	/// </summary>
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.DataAccess.Objects.UserDocument, HomeCloud.IdentityService.Business.Entities.Membership.User}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Membership.User, HomeCloud.IdentityService.DataAccess.Objects.UserDocument}" />
	/// <seealso cref="HomeCloud.Core.ITypeConverter{HomeCloud.IdentityService.Business.Entities.Membership.User, HomeCloud.IdentityService.Business.Entities.Membership.User}" />
	public class UserConverter : ITypeConverter<UserDocument, User>, ITypeConverter<User, UserDocument>, ITypeConverter<User, User>
	{
		#region ITypeConverter<UserDocument, User> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public User Convert(UserDocument source, User target)
		{
			target.ID = source.ID;
			target.Username = source.Username;
			target.FirstName = source.FirstName;
			target.LastName = source.LastName;
			target.Role = (Role)source.Role;

			return target;
		}

		#endregion

		#region ITypeConverter<User, UserDocument> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public UserDocument Convert(User source, UserDocument target)
		{
			target.ID = source.ID;
			target.Username = source.Username;
			target.Password = source.Password;
			target.FirstName = source.FirstName;
			target.LastName = source.LastName;
			target.Role = (int)source.Role;

			return target;
		}

		#endregion

		#region ITypeConverter<User, User> Implementations

		/// <summary>
		/// Converts the instance of <see cref="!:TSource" /> type to the instance of <see cref="!:TTarget" />.
		/// </summary>
		/// <param name="source">The instance of <see cref="!:TSource" />.</param>
		/// <param name="target">The instance of <see cref="!:TTarget" />.</param>
		/// <returns>
		/// The converted instance of <see cref="!:TTarget" />.
		/// </returns>
		public User Convert(User source, User target)
		{
			target.ID = target.ID == Guid.Empty ? source.ID : target.ID;
			target.Username = target.Username is null ? source.Username : target.Username;
			target.Password = target.Password is null ? source.Password : target.Password;
			target.FirstName = target.FirstName is null ? source.FirstName : target.FirstName;
			target.LastName = target.LastName is null ? source.LastName : target.LastName;
			target.Role = target.Role == Role.Anonymous ? source.Role : target.Role;

			return target;
		}

		#endregion
	}
}
