namespace HomeCloud.IdentityService.Api
{
	#region Usings

	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;

	#endregion

	/// <summary>
	/// Represents the application entry point.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Starts the application with the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		/// <summary>
		/// Configures and builds the web host.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns>The instance of <see cref="IWebHost"/>.</returns>
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}

}
