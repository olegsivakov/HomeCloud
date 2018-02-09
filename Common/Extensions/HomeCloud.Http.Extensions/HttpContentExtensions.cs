namespace HomeCloud.Http.Extensions
{
	#region Usings

	using System.Net.Http;
	using System.Threading.Tasks;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="HttpContent" /> instance.
	/// </summary>
	public static class HttpContentExtensions
	{
		/// <summary>
		/// Serializes the <see cref="HttpContent"/> content to the object of <see cref="T"/> type asynchronously.
		/// </summary>
		/// <typeparam name="T">The type of object to serialize to.</typeparam>
		/// <param name="content">The <see cref="HttpContent"/> content.</param>
		public async static Task<T> ReadAsAsync<T>(this HttpContent content)
		{
			string json = await content.ReadAsStringAsync();
			T value = JsonConvert.DeserializeObject<T>(json);
			return value;
		}
	}
}
