namespace HomeCloud.DataStorage.Api.Binders
{
	using Microsoft.AspNetCore.Mvc.Formatters;
	using Microsoft.AspNetCore.Mvc.Internal;
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	#region Usings

	using System.Collections.Generic;

	#endregion

	public class DataModelBinderProvider : IModelBinderProvider
	{
		private readonly IList<IInputFormatter> formatters;
		private readonly IHttpRequestStreamReaderFactory readerFactory;

		public DataModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
		{
			this.formatters = formatters;
			this.readerFactory = readerFactory;
		}

		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			return new DataModelBinder(formatters, readerFactory);
		}
	}
}
