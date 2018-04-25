namespace HomeCloud.IO
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Transactions;

	#endregion

	/// <summary>
	/// Provides two-phase commits/rollbacks for a single <see cref="Transaction" />.
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	/// <seealso cref="System.Transactions.IEnlistmentNotification" />
	public sealed class TransactionEnlistment : IEnlistmentNotification, IDisposable
	{
		#region Private Members

		/// <summary>
		/// The operation container
		/// </summary>
		private readonly IList<ITransactionalOperation> operationContainer = new List<ITransactionalOperation>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionEnlistment"/> class.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		public TransactionEnlistment(Transaction transaction)
		{
			transaction.EnlistVolatile(this, EnlistmentOptions.None);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Registers the <paramref name="operation" /> in the operation container so that it will be committed or rolled back in along with the other registered operations.
		/// </summary>
		/// <param name="operation">The operation.</param>
		public void EnlistOperation(ITransactionalOperation operation)
		{
			operation.Execute();

			this.operationContainer.Add(operation);
		}

		#endregion

		#region IEnlistmentNotification Implementations

		/// <summary>
		/// Notifies an enlisted object that a transaction is being committed.
		/// </summary>
		/// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment"></see> object used to send a response to the transaction manager.</param>
		public void Commit(Enlistment enlistment)
		{
			this.Dispose();

			enlistment.Done();
		}

		/// <summary>
		/// Notifies an enlisted object that the status of a transaction is in doubt.
		/// </summary>
		/// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment"></see> object used to send a response to the transaction manager.</param>
		public void InDoubt(Enlistment enlistment)
		{
			this.Rollback(enlistment);
		}

		/// <summary>
		/// Notifies an enlisted object that a transaction is being prepared for commitment.
		/// </summary>
		/// <param name="preparingEnlistment">A <see cref="T:System.Transactions.PreparingEnlistment"></see> object used to send a response to the transaction manager.</param>
		public void Prepare(PreparingEnlistment preparingEnlistment)
		{
			preparingEnlistment.Prepared();
		}

		/// <summary>
		/// Notifies an enlisted object that a transaction is being rolled back (aborted).
		/// </summary>
		/// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment"></see> object used to send a response to the transaction manager.</param>
		/// <exception cref="TransactionException">Failed to roll back.</exception>
		public void Rollback(Enlistment enlistment)
		{
			try
			{
				for (int index = this.operationContainer.Count - 1; index >= 0; --index)
				{
					this.operationContainer[index].Rollback();
				}

				this.Dispose();
			}
			catch (Exception exception)
			{
				throw new TransactionException("Failed to roll back transaction.", exception);
			}

			enlistment.Done();
		}

		#endregion

		#region IDisposable Implementations

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Parallel.ForEach(operationContainer, (item) =>
			{
				(item as IDisposable)?.Dispose();
			});

			this.operationContainer.Clear();
		}

		#endregion
	}
}