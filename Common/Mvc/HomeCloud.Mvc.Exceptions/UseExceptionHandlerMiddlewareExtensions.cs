namespace HomeCloud.Mvc.Exceptions
{
	#region Usings

	using Microsoft.AspNetCore.Builder;

	#endregion

	/// <summary>
	/// Provides extension methods for exception handling middleware.
	/// </summary>
	public static class UseExceptionHandlerMiddlewareExtensions
	{
		/// <summary>
		/// Adds exception handling middleware to application request pipeline.
		/// </summary>
		/// <param name="builder">The application builder.</param>
		/// <returns>the instance of <see cref="IApplicationBuilder"/>.</returns>
		public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}
