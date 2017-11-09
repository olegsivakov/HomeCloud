namespace HomeCloud.Validation
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;

	using HomeCloud.Exceptions;

	#endregion

	/// <summary>
	/// Represents the instance that contains the results of the validation.
	/// </summary>
	public class ValidationResult
	{
		/// <summary>
		/// Gets the value indicating whether the validation is failed.
		/// </summary>
		/// <value>
		/// <c>True</c> if the validation has been passed. Otherwise, it returns <c>false</c>.
		/// </value>
		public bool IsValid => (this.Errors?.Count()).GetValueOrDefault() == 0;

		/// <summary>
		/// Gets or sets the list of validation errors.
		/// </summary>
		/// <value>
		/// The validation errors.
		/// </value>
		public IEnumerable<ValidationException> Errors { get; set; }

		/// <summary>
		/// Concatenates two instances of <see cref="ValidationResult>"/>.
		/// </summary>
		/// <param name="first">The first instance of <see cref="ValidationResult"/>.</param>
		/// <param name="second">The second instance of <see cref="ValidationResult"/>.</param>
		/// <returns>
		/// The result of the concatenation.
		/// </returns>
		public static ValidationResult operator +(ValidationResult first, ValidationResult second)
		{
			return new ValidationResult()
			{
				Errors = (first?.Errors ?? Enumerable.Empty<ValidationException>()).Union(second?.Errors ?? Enumerable.Empty<ValidationException>()).ToList()
			};
		}
	}
}
