namespace HomeCloud.Core
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Represents the execution result of service operation.
	/// </summary>
	public class ServiceResult
	{
		/// <summary>
		/// Gets a value indicating whether the execution result is succeeded.
		/// </summary>
		/// <value>
		///   <c>true</c> if the execution result is succeed; otherwise, <c>false</c>.
		/// </value>
		public bool IsSuccess => (this.Errors?.Count()).GetValueOrDefault() == 0;

		/// <summary>
		/// Gets or sets the list of errors occurred.
		/// </summary>
		/// <value>
		/// The list of errors.
		/// </value>
		public IEnumerable<Exception> Errors { get; set; }
	}
}
