namespace HomeCloud.Mvc.Exceptions
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Exceptions;
	using HomeCloud.Http;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Logging;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	#endregion

	/// <summary>
	/// Represents middleware that handles the exceptions.
	/// </summary>
	public sealed class ExceptionHandlerMiddleware
	{
		#region Private Members

		/// <summary>
		/// The <see cref="RequestDelegate"/> member.
		/// </summary>
		private readonly RequestDelegate next = null;

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger logger = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
		/// </summary>
		/// <param name="next">The function that processes <see cref="HTTP"/> request.</param>
		/// <param name="loggerFactory">The logger factory.</param>
		public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			this.next = next;
			this.logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Invokes the <see cref="HTTP"/> request action for the specified context asynchronously.
		/// </summary>
		/// <param name="context">The <see cref="HTTP"/> context.</param>
		/// <returns>The asynchronous operation of <see cref="Task"/>.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await this.next(context);
			}
			catch (NotAuthenticatedException exception)
			{
				await this.ProcessExceptionAsync(
					context,
					exception,
					() =>
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;

						return new HttpExceptionResponse
						{
							StatusCode = context.Response.StatusCode,
							Errors = new List<string>() { exception.Message }
						};
					});
			}
			catch (NotAuthorizedException exception)
			{
				await this.ProcessExceptionAsync(
					context,
					exception,
					() =>
					{
						context.Response.StatusCode = StatusCodes.Status403Forbidden;

						return new HttpExceptionResponse
						{
							StatusCode = context.Response.StatusCode,
							Errors = new List<string>() { exception.Message }
						};
					});
			}
			catch (NotFoundException exception)
			{
				await this.ProcessExceptionAsync(
					context,
					exception,
					() =>
					{
						context.Response.StatusCode = StatusCodes.Status404NotFound;

						return new HttpExceptionResponse
						{
							StatusCode = context.Response.StatusCode,
							Errors = new List<string>() { exception.Message }
						};
					});
			}
			catch (Exception exception)
			{
				await this.ProcessExceptionAsync(
					context,
					exception,
					() =>
					{
						context.Response.StatusCode = StatusCodes.Status500InternalServerError;

						return new HttpExceptionResponse
						{
							StatusCode = context.Response.StatusCode,
							Errors = new List<string>() { exception.InnerException is null ? exception.Message : $"{exception.Message}: {exception.InnerException.Message}" }
						};
					});
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Processes the exception asynchronously.
		/// </summary>
		/// <param name="context">The <see cref="HTTP"/> context.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="action">The action executed to create <see cref="HttpExceptionResponse"/>.</param>
		/// <returns>The asynchronous operation of <see cref="Task"/>.</returns>
		private async Task ProcessExceptionAsync(HttpContext context, Exception exception, Func<HttpExceptionResponse> action)
		{
			if (context.Response.HasStarted)
			{
				this.logger.LogWarning("The response has already started, the exception middleware will not be executed");

				throw exception;
			}

			context.Response.ContentType = MimeTypes.Application.Json;

			string json = JsonConvert.SerializeObject(
				action(),
				new JsonSerializerSettings
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});

			await context.Response.WriteAsync(json);

			this.logger.LogError(0, exception, json);
		}

		#endregion
	}
}
