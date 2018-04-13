namespace HomeCloud.Mvc.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Reflection;

	using HomeCloud.Mvc.Exceptions;

	using Microsoft.AspNetCore.Mvc.Controllers;

	#endregion

	/// <summary>
	/// Provides extension methods for <see cref="ControllerActionDescriptor"/>.
	/// </summary>
	public static class ControllerActionDescriptorExtensions
	{
		/// <summary>
		/// Validates the action input arguments.
		/// </summary>
		/// <param name="descriptor">The action descriptor.</param>
		/// <param name="arguments">The action arguments.</param>
		/// <returns>
		/// The model of <see cref="HttpExceptionResponse" /> type in case if validation failed. Otherwise it returns <see cref="null" />.
		/// </returns>
		public static HttpExceptionResponse ValidateArguments(this ControllerActionDescriptor descriptor, IDictionary<string, object> arguments)
		{
			IList<string> errors = new List<string>();

			IEnumerable<ParameterInfo> parameters = descriptor.MethodInfo.GetParameters();
			foreach (var parameter in parameters)
			{
				object value = arguments.ContainsKey(parameter.Name) ? arguments[parameter.Name] : (parameter.ParameterType.IsValueType ? Activator.CreateInstance(parameter.ParameterType) : null);

				IEnumerable<ValidationAttribute> attributes = parameter.GetCustomAttributes<ValidationAttribute>(true);
				foreach (ValidationAttribute attribute in attributes)
				{
					if (!attribute.IsValid(value))
					{
						errors.Add(attribute.FormatErrorMessage(parameter.Name));
					}
				}
			}

			return errors.Count == 0 ? null : new HttpExceptionResponse() { Messages = errors };
		}
	}
}
