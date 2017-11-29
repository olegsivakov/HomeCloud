namespace HomeCloud.Core
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Exposes the enumerator that supports iteration over the subset of strongly-typed collection.
	/// </summary>
	/// <typeparam name="T">The type of item.</typeparam>
	/// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
	/// <seealso cref="HomeCloud.Core.IPaginable" />
	public interface IPaginable<out T> : IEnumerable<T>, IPaginable
	{
	}
}
