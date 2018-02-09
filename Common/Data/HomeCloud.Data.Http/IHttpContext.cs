namespace HomeCloud.Data.Http
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Represents the context object to access to the data through <see cref="HTTP/HTTPS"/> protocol.
	/// </summary>
	public interface IHttpContext : IDisposable
	{
		/// <summary>
		/// Sends <see cref="GET" /> request to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data to get in the response.</typeparam>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>The asynchronous operation.</returns>
		Task<T> GetAsync<T>(string uri);

		/// <summary>
		/// Sends <see cref="JSON-based"/> <see cref="POST"/> request to the specified resource asynchronously.
		/// </summary>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri"/>.</param>
		/// <returns>The asynchronous operation.</returns>
		Task PostAsJsonAsync(object data, string uri);

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="POST" /> request to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data in the response</typeparam>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>The instance of <see cref="T"/>.</returns>
		Task<T> PostAsJsonAsync<T>(object data, string uri);

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="PUT" /> request to the specified resource asynchronously.
		/// </summary>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>The asynchronous operation.</returns>
		Task PutAsJsonAsync(object data, string uri);

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="PUT" /> request to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data in the response</typeparam>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>The instance of <see cref="T"/>.</returns>
		Task<T> PutAsJsonAsync<T>(object data, string uri);

		/// <summary>
		/// Sends <see cref="DELETE" /> request to the specified resource asynchronously.
		/// </summary>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>The asynchronous operation.</returns>
		Task DeleteAsJsonAsync(string uri);
	}
}
