using System;

namespace HomeCloud.Data.Http
{
	/// <summary>
	/// Represents the options to configure context for access to <see cref="Http/HTTPS"/> resource.
	/// </summary>
	public class HttpOptions
	{
		/// <summary>
		/// Gets or sets the resource base address.
		/// </summary>
		/// <value>
		/// The base address.
		/// </value>
		public string BaseAddress { get; set; }

		/// <summary>
		/// Gets or sets the time to idle of the resource to respond.
		/// </summary>
		/// <value>
		/// The timeout.
		/// </value>
		public TimeSpan? Timeout { get; set; }
	}
}
