namespace HomeCloud.DataStorage.Api.Controllers
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using Controller = HomeCloud.Api.Mvc.ControllerBase;
	using HomeCloud.DataStorage.Business.Entities;
	using HomeCloud.Api.Mvc;
	using System.Collections.Generic;
	using HomeCloud.Mapping;
	using HomeCloud.Core.Extensions;

	public abstract class ControllerBase : Controller
	{
		/// <summary>
		/// The <see cref="IMapper"/> mapper.
		/// </summary>
		private readonly IMapper mapper = null;

		protected ControllerBase(IMapper mapper)
		{
			this.mapper = mapper;
		}

		//protected async Task<IActionResult> HttpGet<TData, TModel>(int offset, int limit, Func<Task<ServiceResult<IEnumerable<TData>>>> dataAction, Func<IEnumerable<TData>, Task<IEnumerable<TModel>>> modelAction)
		//	where TModel : IViewModel
		//{
		//	return await this.HttpGet(offset, limit, async () =>
		//	{
		//		ServiceResult<IEnumerable<TData>> result = await dataAction();
		//		if (!result.IsSuccess)
		//		{
		//			ErrorViewModel error = await this.mapper.MapNewAsync<ServiceResult, ErrorViewModel>(result);

		//			return this.UnprocessableEntity(error);
		//		}

		//		IEnumerable<TModel> model = await modelAction(result.Data);

		//		return this.Ok(model);
		//	});
		//}
	}
}
