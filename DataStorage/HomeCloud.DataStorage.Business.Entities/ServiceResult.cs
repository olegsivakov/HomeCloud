namespace HomeCloud.DataStorage.Business.Entities
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	#endregion

	/// <summary>
	/// Represents the result of service operation execution.
	/// </summary>
	public class ServiceResult
	{
		/// <summary>
		/// Gets a value indicating whether the execution result is successed.
		/// </summary>
		/// <value>
		///   <c>true</c> if the execution result is successed; otherwise, <c>false</c>.
		/// </value>
		public bool IsSuccess => (this.Errors?.Count()).GetValueOrDefault() == 0;

		/// <summary>
		/// Gets or sets the list of errors occured.
		/// </summary>
		/// <value>
		/// The list of errors.
		/// </value>
		public IEnumerable<Exception> Errors { get; set; }
	}
}
