namespace HomeCloud.Api.Http
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Defines a contract that represents the result of <see cref="HTTP"/> method.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.IActionResult" />
	public interface IHttpMethodResult : IActionResult
	{
		/// <summary>
		/// Gets a value indicating whether the <see cref="HTTP"/> method has errors.
		/// </summary>
		/// <value>
		/// <c>true</c> if <see cref="HTTP"/> method has errors; otherwise, <c>false</c>.
		/// </value>
		bool HasErrors { get; }

		/// <summary>
		/// Gets or sets the list of errors.
		/// </summary>
		/// <value>
		/// The list of <see cref="Exception"/>.
		/// </value>
		IEnumerable<Exception> Errors { get; set; }

		/// <summary>
		/// Returns the <see cref="IActionResult"/> that represents the current <see cref="HTTP"/> method.
		/// </summary>
		/// <returns>The instance of <see cref="IActionResult"/>.</returns>
		IActionResult ToActionResult();
	}
}
