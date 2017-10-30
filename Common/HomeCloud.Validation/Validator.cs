namespace HomeCloud.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	public abstract class Validator<T> : IValidator<T>
	{
		#region Private Members

		private readonly IList<IValidationRule<T>> rules = new List<IValidationRule<T>>();

		#endregion

		#region IValidator<T> Implementations

		public ValidationResult Validate(T entity)
		{
			return new ValidationResult();
		}

		#endregion

		#region Protected Methods

		protected IValidationRule<T> If(Func<T, bool> rule)
		{
			IValidationRule<T> result = new ValidationRule<T>(rule);

			this.rules.Add(result);

			return result;
		}

		#endregion
	}
}
