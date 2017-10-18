namespace HomeCloud.DataAccess.Components.Factories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Services.Factories;

	#endregion

	/// <summary>
	/// Defines methods to distribute factories of <see cref="ICommandHandlerFactory"/> type.
	/// </summary>
	public sealed class CommandHandlerFactory : ICommandHandlerFactory
	{
		#region Private Members

		/// <summary>
		/// The factory container.
		/// </summary>
		private readonly IDictionary<Type, object> container = new Dictionary<Type, object>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandHandlerFactory" /> class.
		/// </summary>
		/// <param name="databaseCommandHandlerFactory">The <see cref="IDbCommandHandlerFactory" /> factory.</param>
		public CommandHandlerFactory(IDbCommandHandlerFactory databaseCommandHandlerFactory = null)
		{
			if (databaseCommandHandlerFactory != null)
			{
				this.container.Add(typeof(IDbCommandHandlerFactory), databaseCommandHandlerFactory);
			}
		}

		#endregion

		#region ICommandHandlerFactory Implementations

		/// <summary>
		/// Gets the command handler factory.
		/// </summary>
		/// <typeparam name="T">The type of factory derived from <see cref="T:HomeCloud.DataAccess.Services.Factories.ICommandHandlerFactory" />.</typeparam>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.DataAccess.Services.Factories.ICommandHandlerFactory" />.
		/// </returns>
		public T GetFactory<T>() where T : ICommandHandlerFactory
		{
			if (!this.container.ContainsKey(typeof(T)))
			{
				return default(T);
			}

			return (T)this.container[typeof(T)];
		}

		#endregion
	}
}
