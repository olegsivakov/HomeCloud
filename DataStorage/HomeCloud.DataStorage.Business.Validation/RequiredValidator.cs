namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System.Threading.Tasks;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.Validation;
	using System;

	#endregion

	/// <summary>
	/// Provides methods to validate required attributes of the instance.
	/// </summary>
	/// <seealso cref="HomeCloud.Validation.Validator{System.Object}" />
	/// <seealso cref="HomeCloud.DataStorage.Business.Validation.IRequiredValidator" />
	public class RequiredValidator : Validator<object>, IRequiredValidator
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RequiredValidator"/> class.
		/// </summary>
		public RequiredValidator()
			: base()
		{
			this.If(obj => obj is null).AddError("The instance is not defined.");
		}

		#endregion

		#region IRequiredValidator Implementations

		/// <summary>
		/// Validates the specified instance of <see cref="Storage"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Storage instance)
		{
			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The storage name is empty.");

			return await this.ValidateAsync((object)instance);
		}

		/// <summary>
		/// Validates the specified instance of <see cref="Catalog"/> type.
		/// </summary>
		/// <param name="instance">The instance to validate.</param>
		/// <returns>The instance of <see cref="ValidationResult"/> indicating whether the specified instance is valid and containing the detailed message about the validation result.</returns>
		public async Task<ValidationResult> ValidateAsync(Catalog instance)
		{
			this.If(obj => string.IsNullOrWhiteSpace(instance.Name)).AddError("The catalog name is empty.");
			this.If(obj => (instance.Parent?.ID).GetValueOrDefault() == Guid.Empty).AddError("The parent catalog is empty.");

			return await this.ValidateAsync((object)instance);
		}

		#endregion
	}
}
