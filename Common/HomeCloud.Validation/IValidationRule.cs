namespace HomeCloud.Validation
{
	#region Usings

	using System;

	#endregion

	public interface IValidationRule<T>
	{
		IValidationRule<T> AddMessage(string message);

		ValidationResult IsSatisfiedBy(T instance);
	}
}
