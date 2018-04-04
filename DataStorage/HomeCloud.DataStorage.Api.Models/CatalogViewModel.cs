namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents catalog view model.
	/// </summary>
	public class CatalogViewModel : DataViewModel
	{
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
		public virtual bool IsExists { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public virtual long Size { get; set; }
	}
}
