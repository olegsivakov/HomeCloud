namespace HomeCloud.DataStorage.Business.Validation
{
	#region Usings

	using System;

	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.DataStorage.Business.Validation.Abstractions;

	using HomeCloud.Validation;

	#endregion

	public class IdentifierRequiredValidator : Validator<Guid>, IIdentifierRequiredValidator
	{
		#region Constructors

		public IdentifierRequiredValidator()
			: base()
		{
			this.If(id => id == Guid.Empty).AddMessage("The identifier is empty.");
		}

		#endregion

		#region IIdentifierRequiredValidator Implementations

		public ValidationResult Validate(Catalog instance)
		{
			return this.Validate((instance?.ID).GetValueOrDefault());
		}

		public ValidationResult Validate(Storage instance)
		{
			return this.Validate((instance?.ID).GetValueOrDefault());
		}

		#endregion
	}
}
