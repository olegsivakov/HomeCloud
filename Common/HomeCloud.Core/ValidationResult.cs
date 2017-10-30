namespace HomeCloud.Core
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	public class ValidationResult
	{
		public bool IsValid { get; set; }

		public IEnumerable<string> Errors { get; set; }
	}
}
