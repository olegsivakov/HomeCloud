namespace HomeCloud.DataStorage.Api.Binders
{
	#region Usings

	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc.Formatters;
	using Microsoft.AspNetCore.Mvc.Internal;
	using HomeCloud.DataStorage.Api.Models;

	#endregion

	public class DataModelBinder : IModelBinder
	{
		private BodyModelBinder defaultBinder;

		public DataModelBinder(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory) // : base(formatters, readerFactory)
		{
			defaultBinder = new BodyModelBinder(formatters, readerFactory);
		}

		public async Task BindModelAsync(ModelBindingContext bindingContext)
		{
			// callinng the default body binder
			//await defaultBinder.BindModelAsync(bindingContext);

			if (bindingContext.Result.IsModelSet)
			{
				var data = bindingContext.Result.Model as DataViewModel;
				if (data != null)
				{
					var value = bindingContext.ValueProvider.GetValue("Id").FirstValue;
					int intValue = 0;


					bindingContext.Result = ModelBindingResult.Success(data);
				}

			}

			bindingContext.Result = ModelBindingResult.Success(new object());
		}
	}
}
