namespace HomeCloud.Validation
{
	#region Usings

	using System;

	#endregion

	public class ValidationRule<T> : IValidationRule<T>
	{
		#region Private Members

		private readonly Func<T, bool> rule = null;

		private string message = null;

		#endregion

		public ValidationRule(Func<T, bool> rule)
		{
			this.rule = rule;
		}

		#region IValidationRule<T> Implementations

		#region Public Methods

		public IValidationRule<T> AddMessage(string message)
		{
			this.message = message;

			return this;
		}

		public ValidationResult IsSatisfiedBy(T instance)
		{
			ValidationResult result = new ValidationResult();

			if (this.rule != null && this.rule(instance))
			{
				result.Errors.Add(this.message);
			}

			return result;
		}

		#endregion

		#endregion
	}
}
