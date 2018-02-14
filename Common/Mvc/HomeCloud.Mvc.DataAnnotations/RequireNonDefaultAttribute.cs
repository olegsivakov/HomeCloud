namespace HomeCloud.Mvc.DataAnnotations
{
	#region Usings

	using System;
	using System.ComponentModel.DataAnnotations;

	#endregion

	/// <summary>
	/// Serves the attribute usage instance value to be non-default.
	/// </summary>
	/// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class RequireNonDefaultAttribute : ValidationAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequireNonDefaultAttribute"/> class.
		/// </summary>
		public RequireNonDefaultAttribute()
		: base("The {0} field requires a non-default value.")
		{
		}

		/// <summary>
		/// Checks whether the value allocated in the attribute usage instance is valid.
		/// </summary>
		/// <param name="value">The value of the object to validate.</param>
		/// <returns>
		/// True if the specified value is valid; otherwise, false.
		/// </returns>
		public override bool IsValid(object value)
		{
			return value != null && !Equals(value, Activator.CreateInstance(value.GetType()));
		}
	}
}
