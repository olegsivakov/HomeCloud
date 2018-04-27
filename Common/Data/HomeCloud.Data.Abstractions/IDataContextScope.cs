namespace HomeCloud.Data
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Defines transaction scope.
	/// </summary>
	public interface IDataContextScope : IDisposable
	{
		/// <summary>
		/// Begins the current scope.
		/// </summary>
		void Begin();

		/// <summary>
		/// Commits the changes made within the scope.
		/// </summary>
		void Commit();
	}
}
