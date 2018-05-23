namespace HomeCloud.Core.Extensions
{
	#region Usings

	using System;
	using System.Reflection;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="Type"/> objects.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Determines whether the specified <see cref="Type"/> is primitive.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		/// <c>True</c> if the specified type is primitive; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsPrimitive(this Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return (type.GetGenericArguments()[0]).GetTypeInfo().IsPrimitive();
			}

			return type.IsPrimitive
						|| type.IsEnum
						|| type.Equals(typeof(object))
						|| type.Equals(typeof(string))
						|| type.Equals(typeof(decimal))
						|| type.Equals(typeof(DateTime))
						|| type.Equals(typeof(DateTime?));
		}
	}
}
