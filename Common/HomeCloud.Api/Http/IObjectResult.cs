namespace HomeCloud.Api.Http
{
	/// <summary>
	/// 
	/// </summary>
	public interface IObjectResult
	{
		/// <summary>
		/// Gets or sets the object value.
		/// </summary>
		/// <value>
		/// The object value.
		/// </value>
		object Value { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="HTTP"/> status code.
		/// </summary>
		/// <value>
		/// The <see cref="HTTP"/> status code.
		/// </value>
		int? StatusCode { get; set; }
	}
}
