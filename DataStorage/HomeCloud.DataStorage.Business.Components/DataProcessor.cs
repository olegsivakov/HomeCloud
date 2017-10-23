namespace HomeCloud.DataStorage.Business.Components
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	public abstract class DataProcessor
	{
		#region Constructors

		public DataProcessor(Storage storage)
		{
			this.Storage = storage;
		}

		#endregion

		#region Protected Properties

		protected Storage Storage { get; private set; }

		protected DataProcessor Next { get; private set; }

		#endregion

		#region Public Methods

		public DataProcessor SetNext(DataProcessor next)
		{
			this.Next = next;

			return this.Next;
		}

		public void CreateStorage()
		{
			this.MoveNext(next => next.CreateStorage());
		}

		#endregion

		#region Protected Methods

		protected abstract void ExecuteCreateStorage();

		#endregion

		#region Private Methods

		private void MoveNext(Action<DataProcessor> action)
		{
			if (this.Next != null)
			{
				action(this.Next);
			}
		}

		#endregion
	}
}
