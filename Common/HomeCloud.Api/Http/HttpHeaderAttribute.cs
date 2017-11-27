namespace HomeCloud.Api.Http
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Marks the property to be used in the response of <see cref="HttpHeadResult{T}"/> as a header with specified header name.
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
		public HttpHeaderAttribute(string name)
		{
			this.Name = name;
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

		#endregion
	}
}
