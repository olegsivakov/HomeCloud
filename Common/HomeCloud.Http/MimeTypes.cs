namespace HomeCloud.Http
{
	/// <summary>
	/// Provides <see cref="MIME"/> types strings.
	/// </summary>
	public static class MimeTypes
	{
		/// <summary>
		/// Represents the <see cref="'application/*' MIME"/> types.
		/// </summary>
		public static class Application
		{
			/// <summary>
			/// The <see cref="application/json"/> type.
			/// </summary>
			public const string Json = "application/json";

			/// <summary>
			/// The <see cref="application/octet-stream"/> type.
			/// </summary>
			public const string OctetStream = "application/octet-stream";
		}

		/// <summary>
		/// Represents the <see cref="'multipart/*' MIME"/> types.
		/// </summary>
		public static class Multipart
		{
			/// <summary>
			/// The <see cref="multipart/form-data"/> type.
			/// </summary>
			public const string FormData = "multipart/form-data";
		}
	}
}
