namespace HomeCloud.Mvc
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc;

	#endregion

	/// <summary>
	/// Defines a contract that represents the result of <see cref="HTTP"/> method.
	/// </summary>
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
		/// Gets or sets the <see cref="object"/> value to override the response body.
		/// </summary>
		/// <value>
		/// The  <see cref="Object"/> value to override the response body.
		/// </value>
		object Value { get; set; }

		/// <summary>
		/// Returns the <see cref="IActionResult"/> that represents the current <see cref="HTTP"/> method.
		/// </summary>
		/// <returns>The instance of <see cref="IActionResult"/>.</returns>
		IActionResult ToActionResult();
	}
}
