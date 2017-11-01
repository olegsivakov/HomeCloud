namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;
	using System.IO;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;

	using HomeCloud.Validation;

	using HomeCloud.DataAccess.Services.Factories;
	using Microsoft.Extensions.Options;
	using HomeCloud.DataStorage.Api.Configuration;
	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	public class PresenceValidator : Validator<Guid>, IPresenceValidator
	{
		#region Private Members

		private readonly IDataContextScopeFactory dataContextScopeFactory = null;

		private readonly ConnectionStrings connectionStrings = null;

		#endregion

		#region Constructors

		public PresenceValidator(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
			: base()
		{
			this.dataContextScopeFactory = dataContextScopeFactory;
			this.connectionStrings = connectionStrings?.Value;

			this.If(id => id == Guid.Empty).AddMessage("The unique identifier is empty.");
		}

		#endregion

		#region IPresenceValidator Implementations

		public ValidationResult Validate(Storage instance)
		{
			this.If(id =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return scope.GetRepository<IStorageRepository>().Get(id) is null;
				}
			}).AddMessage("The storage does not exist.");

			return this.Validate(instance.ID);
		}

		public ValidationResult Validate(Catalog instance)
		{
			this.If(id =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings.DataStorageDB))
				{
					return scope.GetRepository<IDirectoryRepository>().Get(id) is null;
				}
			}).AddMessage("The catalog does not exist.");

			this.If(id => string.IsNullOrWhiteSpace(instance.Path)).AddMessage("The catalog path is empty.");
			this.If(id => !Directory.Exists(instance.Path)).AddMessage("The catalog doesn't exist by specified path.");

			return this.Validate(instance.ID);
		}

		#endregion
	}
}
