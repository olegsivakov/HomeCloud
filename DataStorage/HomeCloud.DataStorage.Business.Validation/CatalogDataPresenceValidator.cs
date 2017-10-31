namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataAccess.Services.Factories;

	using HomeCloud.DataStorage.Api.Configuration;

	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;

	using HomeCloud.Validation;

	using Microsoft.Extensions.Options;

	#endregion

	public class CatalogDataPresenceValidator : Validator<Catalog>, ICatalogDataPresenceValidator
	{
		#region Constructors

		public CatalogDataPresenceValidator(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings, IOptionsSnapshot<FileSystem> fileSystem)
			: base()
		{
			this.If(catalog => catalog.ID == Guid.Empty).AddMessage("The identifier of the specified catalog is empty.");
			this.If(catalog =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings?.Value.DataStorageDB))
				{
					return scope.GetRepository<IDirectoryRepository>().Get(catalog.ID) is null;
				}
			}).AddMessage("The specified catalog does not exist.");
		}

		#endregion
	}
}
