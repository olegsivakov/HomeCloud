namespace HomeCloud.DataStorage.Business.Services
{
	#region Usings

	using System;
	using System.IO;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	public class CatalogEntryService : ICatalogEntryService
	{
		public async Task<CatalogEntry> CreateEntryAsync(CatalogEntry entry, Stream stream)
		{
		}

		public async Task<CatalogEntry> GetEntriesAsync(Guid catalogID, int offset = 0, int limit = 20)
		{
		}

		public async Task<CatalogEntry> GetEntryAsync(Guid id, int offset, int limit = 20)
		{
		}

		public async Task<CatalogEntry> DeleteEntryAsync(Guid id)
		{
		}
	}
}
