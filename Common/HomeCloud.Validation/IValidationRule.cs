namespace HomeCloud.Validation
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.Exceptions;

	#endregion

	/// <summary>
	/// Defines methods to handle the rule that is executed to validate the instance of <see cref="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of the instance to apply the validation rule.</typeparam>
	public interface IValidationRule<T>
	{
		/// <summary>
		/// Adds the error returned in case when the current instance of <see cref="T:HomeCloud.Validation.IValidationRule`1" /> determines that the instance of <see cref="!:T" /> is not valid and rule gets <c>true</c>.
		/// </summary>
		/// <param name="exception">The validation exception.</param>
		/// <returns>
		/// The current instance of <see cref="T:HomeCloud.Validation.IValidationRule`1" />.
		/// </returns>
		IValidationRule<T> AddError(ValidationException exception);

		/// <summary>
		/// Adds the error returned in case when the current instance of <see cref="T:HomeCloud.Validation.IValidationRule`1" /> determines that the instance of <see cref="!:T" /> is not valid and rule gets <c>true</c>.
		/// </summary>
		/// <param name="message">The validation message.</param>
		/// <returns>
		/// The current instance of <see cref="T:HomeCloud.Validation.IValidationRule`1" />.
		/// </returns>
		IValidationRule<T> AddError(string message);

		/// <summary>
		/// Determines whether the rule applied to the specified instance is <c>true</c> and the specified instance is not valid.
		/// </summary>
		/// <param name="instance">The instance of <see cref="T"/> to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> containing <c>false</c> value that indicates that the rule has been applied successfully and instance of <see cref="T"/> is not valid. Otherwise it contains <c>true</c>.</returns>
		Task<ValidationResult> IsSatisfiedByAsync(T instance);
	}
}
