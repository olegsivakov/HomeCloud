namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.ComponentModel;

	#endregion

	/// <summary>
	/// Represents <see cref="Catalog" /> data contract.
	/// </summary>
	/// <seealso cref="IChangeTracking" />
	public class Catalog : IChangeTracking
	{
		#region Private Members

		/// <summary>
		/// The unique identifier member.
		/// </summary>
		private Guid id = Guid.Empty;

		/// <summary>
		/// The name member.
		/// </summary>
		private string name = null;

		/// <summary>
		/// The parent identifier member.
		/// </summary>
		private Guid? parentID = null;

		/// <summary>
		/// The storage identifier member.
		/// </summary>
		private Guid storageID = Guid.Empty;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
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
		/// Gets or sets the parent identifier.
		/// </summary>
		/// <value>
		/// The parent identifier.
		/// </value>
		public Guid? ParentID
		{
			get => this.parentID;

			set
			{
				if (this.TrackPropertyChanged(this.parentID, value))
				{
					this.parentID = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the storage identifier.
		/// </summary>
		/// <value>
		/// The storage identifier.
		/// </value>
		public Guid StorageID
		{
			get => this.storageID;

			set
			{
				if (this.TrackPropertyChanged(this.storageID, value))
				{
					this.storageID = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get => this.name;

			set
			{
				if (this.TrackPropertyChanged(this.name, value))
				{
					this.name = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>
		/// The creation date.
		/// </value>
		public DateTime CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		public DateTime UpdatedDate { get; set; }

		#endregion

		#region IChangeTracking Implementations

		/// <summary>
		/// Gets a value indicating whether the object changed status is changed.
		/// </summary>
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
		/// <returns><c>true</c> if the property value has been changed. Otherwise <c>false</c>.</returns>
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
