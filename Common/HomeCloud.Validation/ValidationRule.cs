namespace HomeCloud.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Provides methods to handle the rule that is executed to validate the instance of <see cref="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of the instance to apply the validation rule.</typeparam>
	/// <seealso cref="HomeCloud.Validation.IValidationRule{T}" />
	internal class ValidationRule<T> : IValidationRule<T>
	{
		#region Private Members

		/// <summary>
		/// The rule delegate member.
		/// </summary>
		private readonly Func<T, bool> rule = null;

		/// <summary>
		/// The message
		/// </summary>
		private string message = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationRule{T}"/> class.
		/// </summary>
		/// <param name="rule">The rule delegate.</param>
		public ValidationRule(Func<T, bool> rule)
		{
			this.rule = rule;
		}

		#endregion

		#region IValidationRule<T> Implementations

		/// <summary>
		/// Adds the message returned in case when the current instance of <see cref="T:HomeCloud.Validation.IValidationRule`1" /> determines that the instance of <see cref="!:T" /> is not valid and rule gets <c>true</c>.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>
		/// The current instance of <see cref="T:HomeCloud.Validation.IValidationRule`1" />.
		/// </returns>
		public IValidationRule<T> AddMessage(string message)
		{
			this.message = message;

			return this;
		}

		/// <summary>
		/// Determines whether the rule applied to the specified instance is <c>true</c> and the specified instance is not valid.
		/// </summary>
		/// <param name="instance">The instance of <see cref="!:T" /> to validate.</param>
		/// <returns>
		/// The instance of <see cref="T:HomeCloud.Validation.ValidationResult" /> containing <c>false</c> value that indicates that the rule has been applied successfully and instance of <see cref="!:T" /> is not valid. Otherwise it contains <c>true</c>.
		/// </returns>
		public ValidationResult IsSatisfiedBy(T instance)
		{
			ValidationResult result = new ValidationResult();

			if (this.rule != null && this.rule(instance))
			{
				result.Errors = new List<string>() { this.message };
			}

			return result;
		}

		#endregion
	}
}
