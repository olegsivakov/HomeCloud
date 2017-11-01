namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;

	using HomeCloud.Validation;
	using HomeCloud.DataAccess.Services.Factories;
	using HomeCloud.DataStorage.Api.Configuration;
	using Microsoft.Extensions.Options;
	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;
	using System.IO;

	#endregion

	public class UniqueValidator : Validator<Guid>, IUniqueValidator
	{
		#region Private Members

		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		private readonly ConnectionStrings connectionStrings = null;

		#endregion

		#region Constructors

		public UniqueValidator(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
			: base()
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.If(id => id == Guid.Empty).AddMessage("The unique identifier is empty.");
		}

		#endregion

		#region IUniqueValidator Implementations

		public ValidationResult Validate(Storage instance)
		{
			this.If(id =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return scope.GetRepository<IStorageRepository>().Get(id) != null;
				}
			}).AddMessage("The storage already exist.");

			return this.Validate(instance.ID);
		}

		public ValidationResult Validate(Catalog instance)
		{
			this.If(id =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return scope.GetRepository<IDirectoryRepository>().Get(id) != null;
				}
			}).AddMessage("The catalog already exists.");

			this.If(id => string.IsNullOrWhiteSpace(instance.Path)).AddMessage("The catalog path is empty.");
			this.If(id => Directory.Exists(instance.Path)).AddMessage("The catalog already exists by specified path.");

			return this.Validate(instance.ID);
		}

		#endregion
	}
}
