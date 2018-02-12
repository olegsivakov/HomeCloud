namespace HomeCloud.Data.Http
{
	/// <summary>
	/// Defines methods to handle the data of <see cref="T" /> type located on the remote <see cref="Http/HTTPS" /> resource.
	/// </summary>
	/// <typeparam name="T">The type of data/</typeparam>
	/// <seealso cref="HomeCloud.Data.Http.IHttpRepository" />
	/// <seealso cref="HomeCloud.Data.IRepository{T}" />
	public interface IHttpRepository<T> : IHttpRepository, IRepository<T>
	{
	}
}
