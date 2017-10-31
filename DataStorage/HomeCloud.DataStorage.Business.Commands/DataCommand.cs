namespace HomeCloud.DataStorage.Business.Commands
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Providers;

	#endregion

	/// <summary>
	/// Represents the command that executes the specified action against the data provided by <see cref="IDataProvider"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.Business.Commands.ActionCommand" />
	public class DataCommand : ActionCommand
	{
		#region Private Members

		/// <summary>
		/// The data provider member.
		/// </summary>
		private readonly IDataProvider provider = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataCommand"/> class.
		/// </summary>
		/// <param name="provider">The data provider.</param>
		/// <param name="executeAction">The action to execute.</param>
		/// <param name="undoAction">The action to revert command execution result.</param>
		public DataCommand(IDataProvider provider, Action<IDataProvider> executeAction, Action<IDataProvider> undoAction)
			: base(() => executeAction(provider), () => undoAction(provider))
		{
			this.provider = provider;
		}

		#endregion

		#region ICommand Implementations

		#region Public Methods

		/// <summary>
		/// Executes the command.
		/// </summary>
		public override void Execute()
		{
			if (this.provider != null)
			{
				base.Execute();
			}
		}

		/// <summary>
		/// Reverts the command results to previous state.
		/// </summary>
		public override void Undo()
		{
			if (this.provider != null)
			{
				base.Undo();
			}
		}

		#endregion

		#endregion
	}
}
