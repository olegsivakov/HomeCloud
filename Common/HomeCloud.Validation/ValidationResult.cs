namespace HomeCloud.Validation
{
	#region Usings

	using System.Collections.Generic;
	using System.Linq;

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
		public IList<string> Errors { get; set; } = new List<string>();
	}
}
