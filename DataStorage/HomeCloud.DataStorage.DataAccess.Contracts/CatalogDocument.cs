namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.ComponentModel;

	using HomeCloud.DataAccess.Contracts;

	using MongoDB.Bson.Serialization.Attributes;
	using MongoDB.Bson.Serialization.IdGenerators;

	#endregion

	/// <summary>
	/// Represents catalog document.
	/// </summary>
	/// <seealso cref="HomeCloud.DataAccess.Contracts.IDocument" />
	/// <seealso cref="System.ComponentModel.IChangeTracking" />
	[DocumentCollection(CollectionName = "catalogs")]
	public class CatalogDocument : IDocument, IChangeTracking
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
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[BsonId(IdGenerator = typeof(GuidGenerator))]
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

		#region IChangeTracking Implementations

		/// <summary>
		/// Gets a value indicating whether the object status is changed.
		/// </summary>
		[BsonIgnore]
		public bool IsChanged { get; private set; }

		/// <summary>
		/// Resets the object’s state to unchanged by accepting the modifications.
		/// </summary>
		public void AcceptChanges()
		{
			this.IsChanged = false;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Tracks whether the property value has been changed and set it to <see cref="IsChanged" />.
		/// </summary>
		/// <typeparam name="T">The type of the property value.</typeparam>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns><c>true</c> if property value has been changed. Otherwise <c>false</c>.</returns>
		private bool TrackPropertyChanged<T>(T oldValue, T newValue)
		{
			if ((oldValue == null && newValue == null) || (!oldValue?.Equals(newValue) ?? true))
			{
				this.IsChanged = true;

				return true;
			}

			return false;
		}

		#endregion
	}
}
