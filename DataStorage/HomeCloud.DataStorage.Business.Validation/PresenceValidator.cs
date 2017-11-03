namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;
	using System.IO;
	using System.Threading.Tasks;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using HomeCloud.Validation;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to validate whether the specified instance already exists.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Guid}" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IPresenceValidator" />
	public class PresenceValidator : Validator<Guid>, IPresenceValidator
	{
		#region Private Members

		/// <summary>
		/// The data context scope factory
		/// </summary>
		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		/// <summary>
		/// The connection strings
		/// </summary>
		private readonly ConnectionStrings connectionStrings = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PresenceValidator"/> class.
		/// </summary>
		/// <param name="dataContextScopeFactory">The data context scope factory.</param>
		/// <param name="connectionStrings">The connection strings.</param>
		public PresenceValidator(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
			: base()
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.If(id => id == Guid.Empty).AddMessage("The unique identifier is empty.");
		}

		#endregion

		#region IPresenceValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Storage"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Storage instance)
		{
			this.If(async id =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return await scope.GetRepository<IStorageRepository>().GetAsync(id) is null;
				}
			}).AddMessage("The storage does not exist.");

			return await this.ValidateAsync(instance.ID);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Catalog"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Catalog instance)
		{
			this.If(async id =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return await scope.GetRepository<ICatalogRepository>().GetAsync(id) is null;
				}
			}).AddMessage("The catalog does not exist.");

			this.If(id => string.IsNullOrWhiteSpace(instance.Path)).AddMessage("The catalog path is empty.");
			this.If(id => !Directory.Exists(instance.Path)).AddMessage("The catalog doesn't exist by specified path.");

			return await this.ValidateAsync(instance.ID);
		}

		#endregion
	}
}
