namespace HomeCloud.Mvc
{
	#region Usings

	using ControllerBase = HomeCloud.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Implements a contract that represents the result of <see cref="HTTP HEAD" /> method.
	/// </summary>
	/// <seealso cref="HomeCloud.Mvc.HttpGetResult" />
	public sealed class HttpHeadResult : HttpGetResult
	{
		#region Contstructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpHeadResult"/> class.
		/// </summary>
		/// <param name="controller">The controller.</param>
		public HttpHeadResult(ControllerBase controller)
			: base(controller)
		{
		}

		#endregion
	}
}
