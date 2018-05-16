namespace HomeCloud.IdentityService.Business.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.Core;
	using HomeCloud.Validation;

	#endregion

	/// <summary>
	/// Provides methods to create the validation factories of <see cref="IServiceFactory{T}"/> type.
	/// </summary>
	public class ValidationServiceFactory : IValidationServiceFactory
	{
		#region Private Members

		/// <summary>
		/// The service factory container.
		/// </summary>
		private IDictionary<Type, object> container = new Dictionary<Type, object>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationServiceFactory" /> class.
		/// </summary>
		public ValidationServiceFactory(
			IServiceFactory<IClientValidator> clientValidationFactory,
			IServiceFactory<IApiResourceValidator> apiResourceValidationFactory,
			IServiceFactory<IGrantValidator> grantValidationFactory,
			IServiceFactory<IUserValidator> userValidationFactory)
		{
			this.container.Add(typeof(IServiceFactory<IClientValidator>), clientValidationFactory);
			this.container.Add(typeof(IServiceFactory<IApiResourceValidator>), apiResourceValidationFactory);
			this.container.Add(typeof(IServiceFactory<IGrantValidator>), grantValidationFactory);
			this.container.Add(typeof(IServiceFactory<IUserValidator>), userValidationFactory);
		}

		#endregion

		#region IValidationServiceFactory Implementations

		/// <summary>
		/// Gets the validation factory of <see cref="T:HomeCloud.Core.IServiceFactory`1" />.
		/// </summary>
		/// <typeparam name="T">The type of validator which creation is handled by <see cref="T:HomeCloud.Core.IServiceFactory`1" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Core.IServiceFactory`1" />.
		/// </returns>
		public IServiceFactory<T> GetFactory<T>() where T : IValidator
		{
			Type type = typeof(IServiceFactory<T>);

			if (!this.container.ContainsKey(type))
			{
				return null;
			}

			return this.container[type] as IServiceFactory<T>;
		}

		#endregion
	}
}
