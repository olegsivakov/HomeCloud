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
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public virtual SizeViewModel Size { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is catalog.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is catalog; otherwise, <c>false</c>.
		/// </value>
		public override bool IsCatalog
		{
			get => base.IsCatalog = true;
			set => base.IsCatalog = true;
		}
	}
}
