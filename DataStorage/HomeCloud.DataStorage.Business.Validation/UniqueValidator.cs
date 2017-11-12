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

	using HomeCloud.Exceptions;
	using HomeCloud.Validation;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Provides methods to validate whether the specified instance is unique.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Guid}" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IUniqueValidator" />
	public class UniqueValidator : Validator<Guid>, IUniqueValidator
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
		/// Initializes a new instance of the <see cref="UniqueValidator"/> class.
		/// </summary>
		/// <param name="dataContextScopeFactory">The data context scope factory.</param>
		/// <param name="connectionStrings">The connection strings.</param>
		public UniqueValidator(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
			: base()
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;
		}

		#endregion

		#region IUniqueValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Storage"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Storage instance)
		{
			this.If(async id =>
			{
				if (id == Guid.Empty)
				{
					return false;
				}

				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return await scope.GetRepository<IStorageRepository>().GetAsync(id) != null;
				}
			}).AddError(new AlreadyExistsException("The storage already exists."));

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
				if (id == Guid.Empty)
				{
					return false;
				}

				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return await scope.GetRepository<ICatalogRepository>().GetAsync(id) != null;
				}
			}).AddError(new AlreadyExistsException("The catalog already exists."));

			this.If(id => !string.IsNullOrWhiteSpace(instance.Path) && Directory.Exists(instance.Path)).AddError(new AlreadyExistsException("The catalog already exists by specified path."));

			return await this.ValidateAsync(instance.ID);
		}

		#endregion
	}
}
