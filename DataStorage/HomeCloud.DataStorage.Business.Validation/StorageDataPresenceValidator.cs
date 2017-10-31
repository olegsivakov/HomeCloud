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

	public class StorageDataPresenceValidator : Validator<Storage>, IStorageDataPresenceValidator
	{
		#region Constructors

		public StorageDataPresenceValidator(IDataContextScopeFactory dataContextScopeFactory, IOptionsSnapshot<ConnectionStrings> connectionStrings)
			: base()
		{
			this.If(storage => storage.ID == Guid.Empty).AddMessage("The identifier of the specified storage is empty.");
			this.If(storage =>
			{
				using (IDbContextScope scope = dataContextScopeFactory.CreateDbContextScope(connectionStrings?.Value.DataStorageDB))
				{
					return scope.GetRepository<IStorageRepository>().Get(storage.ID) is null;
				}
			}).AddMessage("The specified storage does not exist.");
		}

		#endregion
	}
}
