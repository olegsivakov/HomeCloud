namespace HomeCloud.DataStorage.Business.Validation
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
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IValidationServiceFactory" />
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
		/// Initializes a new instance of the <see cref="ValidationServiceFactory"/> class.
		/// </summary>
		/// <param name="storageValidationFactory">The storage validation factory.</param>
		/// <param name="catalogValidationFactory">The catalog validation factory.</param>
		public ValidationServiceFactory(
			IServiceFactory<IStorageValidator> storageValidationFactory,
			IServiceFactory<ICatalogValidator> catalogValidationFactory,
			IServiceFactory<ICatalogEntryValidator> catalogEntryValidationFactory)
		{
			this.container.Add(typeof(IServiceFactory<IStorageValidator>), storageValidationFactory);
			this.container.Add(typeof(IServiceFactory<ICatalogValidator>), catalogValidationFactory);
			this.container.Add(typeof(IServiceFactory<ICatalogEntryValidator>), catalogEntryValidationFactory);
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
