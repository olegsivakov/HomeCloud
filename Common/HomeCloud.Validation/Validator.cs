namespace HomeCloud.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	#endregion

	/// <summary>
	/// Provides basic implementation of instance validator.
	/// </summary>
	/// <typeparam name="T">The type of the instance to validate.</typeparam>
	/// <seealso cref="HomeCloud.Validation.IValidator{T}" />
	public abstract class Validator<T> : IValidator<T>
	{
		#region Private Members

		/// <summary>
		/// The list of validation rules.
		/// </summary>
		private readonly IList<IValidationRule<T>> rules = new List<IValidationRule<T>>();

		#endregion

		#region IValidator<T> Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="!:T" /> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Validation.ValidationResult" /> indicating whether the specified instance is valid and containing the detailed message about the validation result.
		/// </returns>
		public async Task<ValidationResult> ValidateAsync(T instance)
		{
			ValidationResult result = new ValidationResult();

			foreach (IValidationRule<T> rule in this.rules)
			{
				ValidationResult ruleResult = await rule.IsSatisfiedByAsync(instance);
				if (!ruleResult.IsValid)
				{
					result += ruleResult;
				}
			}

			return result;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Wraps the creation of validation rule and adds it to the list of ones to execute.
		/// </summary>
		/// <param name="rule">The rule delegate.</param>
		/// <returns>The instance of <see cref="IValidationRule{T}"/>.</returns>
		protected virtual IValidationRule<T> If(Func<T, Task<bool>> rule)
		{
			IValidationRule<T> result = new ValidationRule<T>(rule);

			this.rules.Add(result);

			return result;
		}

		/// <summary>
		/// Wraps the creation of validation rule and adds it to the list of ones to execute.
		/// </summary>
		/// <param name="rule">The rule delegate.</param>
		/// <returns>The instance of <see cref="IValidationRule{T}"/>.</returns>
		protected virtual IValidationRule<T> If(Func<T, bool> rule)
		{
			IValidationRule<T> result = new ValidationRule<T>((instance) => Task.Run(() => rule(instance)));

			this.rules.Add(result);

			return result;
		}

		#endregion
	}
}
