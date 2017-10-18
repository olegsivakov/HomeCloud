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
	/// Provides extension methods to extend <see cref="IDbCommand"/>.
	/// </summary>
	public static class DbCommandExtensions
	{
		/// <summary>
		/// Serializes and sets the object to a property of specified <see cref="IDbCommand"/> command as a JSON string.
		/// </summary>
		/// <typeparam name="TCommand">The type of the command derived from <see cref="IDbCommand"/>.</typeparam>
		/// <typeparam name="TJson">The type of the object to serialize.</typeparam>
		/// <param name="command">The <see cref="IDbCommand"/> command.</param>
		/// <param name="property">The <see cref="IDbCommand"/> command property.</param>
		/// <param name="jsonObject">The JSON object to set.</param>
		public static void SetCommandJsonProperty<TCommand, TJson>(this TCommand command, Expression<Func<TCommand, string>> property, TJson jsonObject) where TCommand : IDbCommand
		{
			string jsonString = JsonConvert.SerializeObject(jsonObject);

			var memberSelectorExpression = property.Body as MemberExpression;
			if (memberSelectorExpression != null)
			{
				PropertyInfo propertyInfo = memberSelectorExpression.Member as PropertyInfo;
				if (propertyInfo != null)
				{
					propertyInfo.SetValue(command, jsonString, null);
				}
			}
		}
	}
}
