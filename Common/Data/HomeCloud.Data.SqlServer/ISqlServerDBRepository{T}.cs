namespace HomeCloud.Data.SqlServer
{
	/// <summary>
	/// Represents methods to handle data of <see cref="T" /> stored in <see cref="SqlServer" /> database.
	/// </summary>
	/// <typeparam name="T">The type of data to handle.</typeparam>
	/// <seealso cref="HomeCloud.Data.SqlServer.ISqlServerDBRepository" />
	/// <seealso cref="HomeCloud.Data.IRepository{T}" />
	public interface ISqlServerDBRepository<T> : ISqlServerDBRepository, IRepository<T>
	{
	}
}
