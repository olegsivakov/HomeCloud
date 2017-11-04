namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents catalog/file view model.
	/// </summary>
	public class DataViewModel : ViewModelBase
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the data is catalog.
		/// </summary>
		/// <value>
		///   <c>true</c> if the data is catalog; otherwise, <c>false</c>.
		/// </value>
		public bool IsCatalog { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the data exists in file system.
		/// </summary>
		/// <value>
		///   <c>true</c> if the data exists; otherwise, it returns <c>false</c>.
		/// </value>
		public bool IsExists { get; set; }
	}
}
