namespace HomeCloud.Http.Extensions
{
	#region Usings

	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="HttpClient"/> instance.
	/// </summary>
	public static class HttpClientExtensions
	{
		/// <summary>
		/// Sends <see cref="POST"/> request as a <see cref="JSON"/> containing <see cref="T"/> data to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data in the request.</typeparam>
		/// <param name="httpClient">The instance of <see cref="HttpClient"/> to handle the operation.</param>
		/// <param name="url">The resource <see cref="URL"/>.</param>
		/// <param name="data">The request data.</param>
		/// <returns>The instance of <see cref="HttpResponseMessage"/>.</returns>
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
			this HttpClient httpClient, string url, T data)
		{
			var dataAsString = JsonConvert.SerializeObject(data);
			var content = new StringContent(dataAsString);
			content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.Application.Json);
			return httpClient.PostAsync(url, content);
		}

		/// <summary>
		/// Sends <see cref="PUT"/> request as a <see cref="JSON"/> containing <see cref="T"/> data to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data in the request.</typeparam>
		/// <param name="httpClient">The instance of <see cref="HttpClient"/> to handle the operation.</param>
		/// <param name="url">The resource <see cref="URL"/>.</param>
		/// <param name="data">The request data.</param>
		/// <returns>The instance of <see cref="HttpResponseMessage"/>.</returns>
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
			this HttpClient httpClient, string url, T data)
		{
			var dataAsString = JsonConvert.SerializeObject(data);
			var content = new StringContent(dataAsString);
			content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypes.Application.Json);
			return httpClient.PutAsync(url, content);
		}
	}
}
