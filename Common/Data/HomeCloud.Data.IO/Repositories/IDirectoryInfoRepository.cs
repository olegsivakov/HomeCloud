namespace HomeCloud.Data.IO.Repositories
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// Defines methods to handle <see cref="DirectoryInfo" /> data.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemRepository{System.IO.DirectoryInfo}" />
	public interface IDirectoryInfoRepository : IFileSystemRepository<DirectoryInfo>
	{
	}
}
