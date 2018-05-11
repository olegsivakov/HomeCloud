namespace HomeCloud.IdentityService.Business.Entities
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents application entity.
	/// </summary>
	public class Application
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the application secrets.
		/// </summary>
		/// <value>
		/// The secrets.
		/// </value>
		public IEnumerable<string> Secrets { get; set; } = new List<string>();

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public ApplicationType Type { get; set; }
	}
}
