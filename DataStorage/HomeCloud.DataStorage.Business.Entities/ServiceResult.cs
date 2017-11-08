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

	/// <summary>
	/// Represents the <see cref="T" /> result of service operation execution.
	/// </summary>
	/// <typeparam name="T">The type of result instance.</typeparam>
	public class ServiceResult<T> : ServiceResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceResult{T}"/> class.
		/// </summary>
		/// <param name="data">The result of <see cref="T"/> type.</param>
		public ServiceResult(T data)
		{
			this.Data = data;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the data result of <see cref="T"/> type.
		/// </summary>
		/// <value>
		/// The data result.
		/// </value>
		public T Data { get; }

		#endregion
	}
}
