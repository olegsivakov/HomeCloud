namespace HomeCloud.Mvc
{
	#region Usings

	using Controller = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP HEAD" /> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.HttpGetResult" />
	public sealed class HttpHeadResult : HttpGetResult
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpHeadResult" /> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="value">The value to override <see cref="!:HTTP" /> body. Can be set to <see cref="T:System.Nullable" /> as default.</param>
		public HttpHeadResult(Controller controller, object value)
			: base(controller, value)
		{
		}

		#endregion
	}
}
