namespace HomeCloud.DataStorage.DataAccess.Contracts
{
	#region Usings

	using System;
	using System.ComponentModel;

	#endregion

	/// <summary>
	/// Represents <see cref="Storage" /> data contract.
	/// </summary>
	public class Storage : IChangeTracking
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
		/// The quota member.
		/// </summary>
		private long quota = 0;

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
		/// Gets or sets the quota.
		/// </summary>
		/// <value>
		/// The quota.
		/// </value>
		public long Quota
		{
			get => this.quota;
			set
			{
				if (this.TrackPropertyChanged(this.quota, value))
				{
					this.quota = value;
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
		/// Gets the object's changed status.
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
		/// Tracks whether the property value has been changed and set it to <see cref="IsChanged"/>.
		/// </summary>
		/// <typeparam name="T">The tpe of the property value.</typeparam>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
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
