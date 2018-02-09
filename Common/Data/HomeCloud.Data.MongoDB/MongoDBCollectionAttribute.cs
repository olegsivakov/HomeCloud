namespace HomeCloud.Data.MongoDB
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents the collection definition for the types derived from <see cref="IDocument"/>.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class MongoDBCollectionAttribute : Attribute
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MongoDBCollectionAttribute" /> class.
		/// </summary>
		/// <param name="name">The collection name.</param>
		public MongoDBCollectionAttribute(string name)
		{
			this.CollectionName = name;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the collection name.
		/// </summary>
		/// <value>
		/// The collection name.
		/// </value>
		public string CollectionName { get; set; }

		#endregion
	}
}
