namespace HomeCloud.DataStorage.DataAccess.Aggregation.Objects
{
	#region Usings

	using System;

	using HomeCloud.Core;
	using HomeCloud.Data.MongoDB;

	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Represents catalog document.
	/// </summary>
	[MongoDBCollection("catalogs")]
	public class CatalogDocument : ChangeTrackingBase
	{
		#region Private Members

		/// <summary>
		/// The unique identifier member.
		/// </summary>
		private Guid id = Guid.Empty;

		/// <summary>
		/// The path member.
		/// </summary>
		private string path = null;

		/// <summary>
		/// The size member.
		/// </summary>
		private long size = 0;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets a value indicating whether the object changed status is changed.
		/// </summary>
		[BsonIgnore]
		public override bool IsChanged
		{
			get => base.IsChanged;
			protected set => base.IsChanged = value;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonId(IdGenerator = typeof(NullIdChecker))]
		[BsonIgnoreIfDefault]
		[BsonRequired]
		public Guid ID
		{
			get => this.id;

			set
			{
				if (this.TrackPropertyChanged(this.id, value))
				{
					this.id = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		[BsonElement("path")]
		[BsonRequired]
		public string Path
		{
			get => this.path;

			set
			{
				if (this.TrackPropertyChanged(this.path, value))
				{
					this.path = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[BsonElement("size")]
		public long Size
		{
			get => this.size;

			set
			{
				if (this.TrackPropertyChanged(this.size, value))
				{
					this.size = value;
				}
			}
		}

		#endregion
	}
}
