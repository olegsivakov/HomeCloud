namespace HomeCloud.Core
{
	#region Usings

	using System.ComponentModel;

	#endregion

	/// <summary>
	/// Provides basic mechanism for tracking object for changes and resseting of the changed status.
	/// </summary>
	/// <seealso cref="System.ComponentModel.IChangeTracking" />
	public abstract class ChangeTrackingBase : IChangeTracking
	{
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

		#region Protected Methods

		/// <summary>
		/// Tracks whether the property value has been changed and set it to <see cref="IsChanged" />.
		/// </summary>
		/// <typeparam name="T">The type of the property value.</typeparam>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		/// <returns><c>true</c> if the property value has been changed. Otherwise <c>false</c>.</returns>
		protected bool TrackPropertyChanged<T>(T oldValue, T newValue)
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
