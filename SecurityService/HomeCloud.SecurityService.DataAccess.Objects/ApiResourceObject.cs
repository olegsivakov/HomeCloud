namespace HomeCloud.SecurityService.DataAccess.Objects
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents <see cref="api"/> resource response object.
	/// </summary>
	public class ApiResourceObject
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the list of claims.
		/// </summary>
		/// <value>
		/// The claims.
		/// </value>
		public IEnumerable<string> Claims { get; set; }

		/// <summary>
		/// Gets or sets the secrets.
		/// </summary>
		/// <value>
		/// The secrets.
		/// </value>
		public IEnumerable<string> Secrets { get; set; }

		/// <summary>
		/// Gets or sets the scopes.
		/// </summary>
		/// <value>
		/// The scopes.
		/// </value>
		public IEnumerable<string> Scopes { get; set; }
	}
}
