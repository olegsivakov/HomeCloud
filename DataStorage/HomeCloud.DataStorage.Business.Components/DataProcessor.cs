namespace HomeCloud.DataStorage.Business.Components
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Entities;

	#endregion

	public abstract class DataProcessor
	{
		#region Private Members

		private Storage storage = null;

		#endregion

		#region Constructors

		public DataProcessor(Storage storage)
		{
			this.storage = storage;
		}

		#endregion

		#region Protected Properties

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

		public void UpdateStorage()
		{
			this.MoveNext(next => next.UpdateStorage());
		}

		public virtual void DeleteStorage()
		{
		}

		public virtual void GetStorages()
		{
		}

		public virtual void CreateCatalog()
		{
		}

		public virtual void UpdateCatalog()
		{
		}

		public virtual void DeleteCatalog()
		{
		}

		public virtual void GetCatalogs()
		{
		}

		public virtual void CreateCatalogEntry()
		{
		}

		public virtual void UpdateCatalogEntry()
		{
		}

		public virtual void DeleteCatalogEntry()
		{
		}

		public virtual void GetCatalogEntry()
		{
		}

		public virtual void GetCatalogEntries()
		{
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
