namespace HomeCloud.Validation
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Marks the implementation to be responsible for object validation.
	/// </summary>
	public interface IValidator
	{
	}

	/// <summary>
	/// Defines methods to validate instance of <see cref="T"/> type.
	/// </summary>
	/// <typeparam name="T">The type of the instance to validate.</typeparam>
	public interface IValidator<T> : IValidator
	{
		IValidationRule<T> If(Func<T, bool> rule);

		/// <summary>
		/// Validates the specified instance of <see cref="T"/> type.
		/// </summary>
		/// <param name="entity">The entity to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		ValidationResult Validate(T entity);
	}
}
