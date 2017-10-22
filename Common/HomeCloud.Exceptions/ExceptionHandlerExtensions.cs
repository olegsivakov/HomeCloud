namespace HomeCloud.Exceptions
{
	#region Usings

	using Microsoft.AspNetCore.Builder;

	#endregion

	/// <summary>
	/// Provides extension methods for exception handling.
	/// </summary>
	public static class ExceptionHandlerExtensions
	{
		/// <summary>
		/// Adds exception handling middleware.
		/// </summary>
		/// <param name="builder">The application builder.</param>
		/// <returns>the instance of <see cref="IApplicationBuilder"/>.</returns>
		public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}
