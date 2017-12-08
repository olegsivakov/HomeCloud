namespace HomeCloud.Api.Http
{
	#region Usings

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="Http HEAD" /> method.
	/// </summary>
	/// <typeparam name="T">The type of the data provided by <see cref="HTTP"/> method </typeparam>
	/// <seealso cref="HomeCloud.Api.Http.HttpGetResult{T}" />
	public class HttpHeadResult<T> : HttpGetResult<T>
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpHeadResult{T}"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpHeadResult(ControllerBase controller)
			: base(controller)
		{
		}

		#endregion
	}
}
