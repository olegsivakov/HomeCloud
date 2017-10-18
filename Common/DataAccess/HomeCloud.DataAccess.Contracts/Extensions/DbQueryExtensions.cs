namespace HomeCloud.DataAccess.Contracts.Extensions
{
	#region Usings

	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	using HomeCloud.DataAccess.Contracts;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Provides extension methods to extend <see cref="IDbQuery"/>.
	/// </summary>
	public static class DbQueryExtensions
	{
		/// <summary>
		/// Serializes and sets the object to a property of specified <see cref="IDbQuery" /> query as a JSON string.
		/// </summary>
		/// <typeparam name="TQuery">The type of the query derived from <see cref="IDbQuery" />.</typeparam>
		/// <typeparam name="TJson">The type of the object to serialize.</typeparam>
		/// <param name="query">The <see cref="IDbQuery" /> query.</param>
		/// <param name="property">The <see cref="IDbQuery" /> query property.</param>
		/// <param name="jsonObject">The JSON object to set.</param>
		public static void SetQueryJsonProperty<TQuery, TJson>(this TQuery query, Expression<Func<TQuery, string>> property, TJson jsonObject) where TQuery : IDbQuery
		{
			string jsonString = JsonConvert.SerializeObject(jsonObject);

			var memberSelectorExpression = property.Body as MemberExpression;
			if (memberSelectorExpression != null)
			{
				PropertyInfo propertyInfo = memberSelectorExpression.Member as PropertyInfo;
				if (propertyInfo != null)
				{
					propertyInfo.SetValue(query, jsonString, null);
				}
			}
		}
	}
}
