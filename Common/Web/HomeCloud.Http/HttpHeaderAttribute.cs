namespace HomeCloud.Http
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;

	#endregion

	/// <summary>
	/// Marks the property to be used in the response as a header with specified header name.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class HttpHeaderAttribute : Attribute
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpHeaderAttribute"/> class.
		/// </summary>
		/// <param name="name">The <see cref="HTTP HEADER"/> name.</param>
		public HttpHeaderAttribute(string name, params string[] httpMethods)
		{
			this.Name = name;

			this.AllowedHttpMethods = httpMethods?.AsEnumerable() ?? new List<string>()
			{
				HttpMethod.Get.Method,
				HttpMethod.Post.Method,
				HttpMethod.Put.Method,
				HttpMethod.Delete.Method,
				HttpMethod.Head.Method
			};
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the header name.
		/// </summary>
		/// <value>
		/// The header name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the collection of HTTP methods the current instance is applicable.
		/// </summary>
		/// <value>
		/// The collection of HTTP methods.
		/// </value>
		public IEnumerable<string> AllowedHttpMethods { get; private set; }

		#endregion
	}
}
