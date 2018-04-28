namespace HomeCloud.Data.IO.Repositories
{
	#region Usings

	using System.IO;

	#endregion

	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="HomeCloud.Data.IO.IFileSystemRepository{System.IO.FileInfo}" />
	public interface IFileInfoRepository : IFileSystemRepository<FileInfo>
	{
		/// <summary>
		/// Saves the specified entity of <see cref="FileInfo" />.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="stream">The stream to write to the file.</param>
		/// <returns>
		/// The instance of <see cref="FileInfo" />.
		/// </returns>
		FileInfo Save(FileInfo entity, Stream stream);
	}
}
