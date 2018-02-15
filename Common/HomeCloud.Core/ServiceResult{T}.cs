namespace HomeCloud.Core
{
	/// <summary>
	/// Represents the <see cref="T" /> result of service operation execution.
	/// </summary>
	/// <typeparam name="T">The type of the result instance.</typeparam>
	/// <seealso cref="HomeCloud.Core.ServiceResult" />
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
