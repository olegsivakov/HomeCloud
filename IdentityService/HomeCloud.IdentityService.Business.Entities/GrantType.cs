namespace HomeCloud.IdentityService.Business.Entities
{
	#region Usings

	using System;

	#endregion

	/// <summary>
	/// Represents enumeration of grant types.
	/// </summary>
	[Flags]
	public enum GrantType
	{
		/// <summary>
		/// The unknown grant type.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// The authorization code grant.
		/// </summary>
		Code = 1,

		/// <summary>
		/// The client credentials grant.
		/// </summary>
		ClientCredentials = 2,

		/// <summary>
		/// The implicit grant.
		/// </summary>
		Implicit = 4,

		/// <summary>
		/// The hybrid grant.
		/// </summary>
		Hybrid = 8,

		/// <summary>
		/// The resource owner password grant.
		/// </summary>
		ResourceOwnerPassword = 16,

		/// <summary>
		/// The code and client credentials grants.
		/// </summary>
		CodeAndClientCredentials = Code | ClientCredentials,

		/// <summary>
		/// The implicit and client credentials grants.
		/// </summary>
		ImplicitAndClientCredentials = Implicit | ClientCredentials,

		/// <summary>
		/// The hybrid and client credentials grants.
		/// </summary>
		HybridAndClientCredentials = Hybrid | ClientCredentials,

		/// <summary>
		/// The resource owner password and client credentials grants.
		/// </summary>
		ResourceOwnerPasswordAndClientCredentials = ResourceOwnerPassword | ClientCredentials
	}
}
