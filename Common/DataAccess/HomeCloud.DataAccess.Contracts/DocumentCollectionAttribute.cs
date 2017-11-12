namespace HomeCloud.DataAccess.Contracts
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the collection definition for the types derived from <see cref="IDocument"/>.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class DocumentCollectionAttribute : Attribute
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentCollectionAttribute"/> class.
		/// </summary>
		public DocumentCollectionAttribute()
		{
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the name of the collection.
		/// </summary>
		/// <value>
		/// The name of the collection.
		/// </value>
		public string CollectionName { get; set; }

		#endregion
	}
}
