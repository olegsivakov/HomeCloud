namespace HomeCloud.DataStorage.DataAccess.Services.Repositories
{
	#region Usings

	using HomeCloud.DataAccess.Services;
	using HomeCloud.DataStorage.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Defines combination of methods to handle file documents.
	/// </summary>
	public interface IFileDocumentRepository : IDocumentRepository<FileDocument>
	{
	}
}
