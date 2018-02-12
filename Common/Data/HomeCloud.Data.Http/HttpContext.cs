namespace HomeCloud.Data.Http
{
	#region Usings

	using System;
	using System.Net.Http;
	using System.Net.Http.Headers;

	using System.Threading.Tasks;

	using HomeCloud.Http;
	using HomeCloud.Http.Extensions;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Represents the context object to access to the data via <see cref="HTTP/HTTPS"/> protocol.
	/// </summary>
	/// <seealso cref="HomeCloud.Data.Http.IHttpContext" />
	public class HttpContext : IHttpContext
	{
		#region Private Fields

		/// <summary>
		/// The <see cref="HttpClient"/>.
		/// </summary>
		private HttpClient httpClient = null;

		/// <summary>
		/// The resource configuration options.
		/// </summary>
		private readonly HttpOptions options = null;

		#endregion

		#region Private Properties

		/// <summary>
		/// Gets the initialized instance of <see cref="HttpClient"/>.
		/// </summary>
		/// <value>
		/// The <see cref="HttpClient"/>.
		/// </value>
		private HttpClient Client => this.httpClient ?? (this.httpClient = this.ConfigureHttpClient());

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpContext" /> class.
		/// </summary>
		/// <param name="accessor">The configuration options accessor.</param>
		/// <exception cref="System.ArgumentNullException">accessor or Value or <see cref="HttpOptions.BaseAddress"/>.</exception>
		public HttpContext(IOptionsSnapshot<HttpOptions> accessor)
		{
			if (accessor is null)
			{
				throw new ArgumentNullException(nameof(accessor));
			}

			if (accessor.Value is null)
			{
				throw new ArgumentNullException(nameof(accessor.Value));
			}

			if (string.IsNullOrWhiteSpace(accessor.Value.BaseAddress))
			{
				throw new ArgumentNullException(nameof(accessor.Value.BaseAddress));
			}

			this.options = accessor.Value;
		}

		#endregion

		#region IHttpClientContext Implementations

		/// <summary>
		/// Sends <see cref="GET" /> request to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data to get in the response.</typeparam>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task<T> GetAsync<T>(string uri)
		{
			return await this.InvokeAsync<T>(
											async client => await client.GetAsync(uri),
											async response => await response.Content.ReadAsAsync<T>());
		}

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="POST" /> request to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data in the response</typeparam>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>
		/// The instance of <see cref="T" />.
		/// </returns>
		public async Task<T> PostAsJsonAsync<T>(object data, string uri)
		{
			return await this.InvokeAsync<T>(
											async client => await client.PostAsJsonAsync(uri, data),
											async response => await response.Content.ReadAsAsync<T>());
		}

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="POST" /> request to the specified resource asynchronously.
		/// </summary>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task PostAsJsonAsync(object data, string uri)
		{
			await this.InvokeAsync<object>(async client => await client.PostAsJsonAsync(uri, data));
		}

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="PUT" /> request to the specified resource asynchronously.
		/// </summary>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task PutAsJsonAsync(object data, string uri)
		{
			await this.InvokeAsync<object>(async client => await client.PutAsJsonAsync(uri, data));
		}

		/// <summary>
		/// Sends <see cref="JSON-based" /> <see cref="PUT" /> request to the specified resource asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of data in the response</typeparam>
		/// <param name="data">The request data.</param>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>
		/// The instance of <see cref="T" />.
		/// </returns>
		public async Task<T> PutAsJsonAsync<T>(object data, string uri)
		{
			return await this.InvokeAsync<T>(
											async client => await client.PutAsJsonAsync(uri, data),
											async response => await response.Content.ReadAsAsync<T>());
		}

		/// <summary>
		/// Sends <see cref="DELETE" /> request to the specified resource asynchronously.
		/// </summary>
		/// <param name="uri">The resource <see cref="Uri" />.</param>
		/// <returns>
		/// The asynchronous operation.
		/// </returns>
		public async Task DeleteAsJsonAsync(string uri)
		{
			await this.InvokeAsync<object>(async client => await client.DeleteAsync(uri));
		}

		#endregion

		#region Implementation of IDisposable interface

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			((IDisposable)this.httpClient)?.Dispose();
		}

		#endregion

		#region Private Implementation

		/// <summary>
		/// Invokes the operation asynchronously.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="operation">The operation.</param>
		/// <param name="actionOnResponse">The action on response.</param>
		/// <exception cref="ArgumentNullException">operation</exception>
		private async Task<T> InvokeAsync<T>(
			Func<HttpClient, Task<HttpResponseMessage>> operation,
			Func<HttpResponseMessage, Task<T>> actionOnResponse = null)
		{
			if (operation == null)
			{
				throw new ArgumentNullException(nameof(operation));
			}

			HttpResponseMessage response = await operation(this.Client);
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException($"Resource server returned an error. StatusCode : {response.StatusCode}");
			}

			if (actionOnResponse != null)
			{
				return await actionOnResponse(response);
			}

			return default(T);
		}

		/// <summary>
		/// Configures the instance of <see cref="HttpClient"/>.
		/// </summary>
		/// <param name="client">The instance of <see cref="HttpClient"/> to configure.</param>
		/// <returns>The configured instance of <see cref="HttpClient"/>.</returns>
		private HttpClient ConfigureHttpClient()
		{
			if (this.httpClient is null)
			{
				this.httpClient = new HttpClient();
			}

			this.httpClient.BaseAddress = new Uri(this.options.BaseAddress);
			this.httpClient.Timeout = this.options.Timeout.HasValue ? this.options.Timeout.Value : this.httpClient.Timeout;

			this.httpClient.DefaultRequestHeaders.Accept.Clear();
			this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypes.Application.Json));

			return this.httpClient;
		}

		#endregion
	}
}
