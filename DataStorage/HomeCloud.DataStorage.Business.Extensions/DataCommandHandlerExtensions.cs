namespace HomeCloud.DataStorage.Business.Extensions
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Handlers;
	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="IDataCommandHandler"/> instance.
	/// </summary>
	public static class DataCommandHandlerExtensions
	{
		/// <summary>
		/// Creates the asynchronous data command executed against each item in the for the list of instances of <see cref="T"/> type.
		/// </summary>
		/// <typeparam name="TDataProvider">The type of the data provider.</typeparam>
		/// <param name="executeAsyncAction">The asynchronous action to execute.</param>
		/// <param name="undoAsyncAction">The asynchronous action to undo.</param>
		/// <returns>
		/// The current instance of <see cref="IDataCommandHandler"/>.
		/// </returns>
		public static IDataCommandHandler CreateAsyncCommandFor<T, TDataProvider>(this IDataCommandHandler handler, IEnumerable<T> items, Func<IDataProvider, T, Task> executeAsyncAction, Func<IDataProvider, T, Task> undoAsyncAction)
			where TDataProvider : IDataProvider
		{
			if (items is null)
			{
				return handler;
			}

			foreach (T item in items)
			{
				handler.CreateAsyncCommand<TDataProvider>(
					provider => executeAsyncAction?.Invoke(provider, item),
					provider => undoAsyncAction?.Invoke(provider, item));
			}

			return handler;
		}
	}
}
