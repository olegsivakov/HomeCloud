namespace HomeCloud.DataStorage.Api.Controllers
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using HomeCloud.Api.Http;
	using HomeCloud.Api.Mvc;

	using HomeCloud.Core;
	using HomeCloud.Core.Extensions;

	using HomeCloud.DataStorage.Api.Models;
	using HomeCloud.DataStorage.Business.Entities;

	using HomeCloud.Mapping;

	using Microsoft.AspNetCore.Mvc;

	using ControllerBase = HomeCloud.Api.Mvc.ControllerBase;

	#endregion

	/// <summary>
	/// Provides methods to process the <see cref="ServiceResult"/> data and expose an action supported by <see cref="IHttpMethodResult"/>.
	/// </summary>
	/// <seealso cref="HomeCloud.Api.Mvc.ControllerBase" />
	public abstract class Controller : ControllerBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Controller"/> class.
		/// </summary>
		/// <param name="mapper">The type mapper.</param>
		protected Controller(IMapper mapper)
		{
			this.Mapper = mapper;
		}

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the type mapper.
		/// </summary>
		/// <value>
		/// The type mapper.
		/// </value>
		protected IMapper Mapper { get; private set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{IEnumerable{TData}}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP GET" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the data which collection of <see cref="IEnumerable{TData}" /> type is being processed.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="result">The instance containing the collection of data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpGetResult<TData, TModel>(ServiceResult<IEnumerable<TData>> result)
			where TModel : class, IViewModel, new()
		{
			return new HttpGetResult<IEnumerable<TModel>>(this)
			{
				Errors = result.Errors,
				Data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null
			};
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{IPaginable{TData}}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP GET" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the data which collection of <see cref="IPaginable{TData}" /> type is being processed.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="result">The instance containing the collection of data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpGetResult<TData, TModel>(ServiceResult<IPaginable<TData>> result)
			where TModel : class, IViewModel, new()
		{
			IEnumerable<TModel> data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null;

			return new HttpGetResult<IEnumerable<TModel>>(this)
			{
				Errors = result.Errors,
				Data = data != null ? new PagedListViewModel<TModel>(data) { TotalCount = result.Data.TotalCount } : null
			};
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{TData}" /> binary data to <see cref="API" /> understandable <see cref="FileResult" /> contract identified and supported by <see cref="HTTP GET" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the binary data to process.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="result">The instance containing the binary exposure.</param>
		/// <returns>
		/// The instance of <see cref="FileResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpGetStreamResult<TData, TModel>(ServiceResult<TData> result)
			where TModel : class, IFileViewModel, new()
		{
			HttpGetStreamResult<TModel> httpResult = new HttpGetStreamResult<TModel>(this)
			{
				Errors = result.Errors,
				Data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null
			};

			return httpResult;
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{TData}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP GET" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the data to process.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="result">The instance containing the data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpGetResult<TData, TModel>(ServiceResult<TData> result)
			where TModel : class, IViewModel, new()
		{
			return new HttpGetResult<TModel>(this)
			{
				Errors = result.Errors,
				Data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null
			};
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{TData}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP HEAD" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the data to process.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="result">The instance containing the data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpHeadResult<TData, TModel>(ServiceResult<TData> result)
			where TModel : class, IViewModel, new()
		{
			HttpHeadResult<TModel> httpResult = new HttpHeadResult<TModel>(this)
			{
				Errors = result.Errors,
				Data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null
			};

			return httpResult;
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{TData}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP POST" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the data to process.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="locationUrlAction">The <see cref="IActionResult"/> action method which route <see cref="URL"/> is required to be presented in the header of the <see cref="HTTP"/> method.</param>
		/// <param name="result">The instance containing the data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpPostResult<TData, TModel>(Func<Guid, Task<IActionResult>> locationUrlAction, ServiceResult<TData> result)
			where TModel : class, IViewModel, new()
		{
			return new HttpPostResult<TModel>(this, locationUrlAction)
			{
				Errors = result.Errors,
				Data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null
			};
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{TData}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP PUT" /> method.
		/// </summary>
		/// <typeparam name="TData">The type of the data to process.</typeparam>
		/// <typeparam name="TModel">The type of the model to expose.</typeparam>
		/// <param name="locationUrlAction">The <see cref="IActionResult" /> action which route <see cref="URL" /> is required to be presented in the header of the <see cref="HTTP" /> method.</param>
		/// <param name="result">The instance containing the data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpPutResult<TData, TModel>(Func<Guid, Task<IActionResult>> locationUrlAction, ServiceResult<TData> result)
			where TModel : class, IViewModel, new()
		{
			return new HttpPutResult<TModel>(this, locationUrlAction)
			{
				Errors = result.Errors,
				Data = result.Data != null ? await this.Mapper.MapNewAsync<TData, TModel>(result.Data) : null
			};
		}

		/// <summary>
		/// Processes the specified <see cref="ServiceResult{TData}" /> data to <see cref="API" /> understandable <see cref="IActionResult" /> contract identified and supported by <see cref="HTTP DELETE" /> method.
		/// </summary>
		/// <param name="result">The instance containing the data.</param>
		/// <returns>
		/// The instance of <see cref="IHttpMethodResult" />.
		/// </returns>
		[NonAction]
		public async Task<IHttpMethodResult> HttpDeleteResult(ServiceResult result)
		{
			return await Task.FromResult(new HttpDeleteResult(this)
			{
				Errors = result.Errors
			});
		}

		#endregion
	}
}
