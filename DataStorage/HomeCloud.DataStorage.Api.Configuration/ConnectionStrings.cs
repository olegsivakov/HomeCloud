namespace HomeCloud.DataStorage.Api.Configuration
{
	/// <summary>
	/// Represents connection string settings.
	/// </summary>
	public class ConnectionStrings
	{
		/// <summary>
		/// Gets or sets the <see cref="DataStorage"/> connection string.
		/// </summary>
		/// <value>
		/// The <see cref="DataStorage"/> connection string.
		/// </value>
		public string DataStorageDB { get; set; }

		/// <summary>
		/// Gets or sets the name of aggregation database.
		/// </summary>
		/// <value>
		/// The name of aggregation database.
		/// </value>
		public string DataAggregationDB { get; set; }
	}
}
