namespace HomeCloud.DataStorage.Api.Filters
{
	#region Usings

	using System;
	using System.Linq;

	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.AspNetCore.Mvc.ModelBinding;

	#endregion

	/// <summary>
	/// Provides filter to disable form value model binding.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IResourceFilter" />
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
	{
		/// <summary>
		/// Executes the resource filter. Called before execution of the remainder of the pipeline.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResourceExecutingContext" />.</param>
		public void OnResourceExecuting(ResourceExecutingContext context)
		{
			var formValueProviderFactory = context.ValueProviderFactories.OfType<FormValueProviderFactory>().FirstOrDefault();
			if (formValueProviderFactory != null)
			{
				context.ValueProviderFactories.Remove(formValueProviderFactory);
			}

			var jqueryFormValueProviderFactory = context.ValueProviderFactories.OfType<JQueryFormValueProviderFactory>().FirstOrDefault();
			if (jqueryFormValueProviderFactory != null)
			{
				context.ValueProviderFactories.Remove(jqueryFormValueProviderFactory);
			}
		}

		/// <summary>
		/// Executes the resource filter. Called after execution of the remainder of the pipeline.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ResourceExecutedContext" />.</param>
		public void OnResourceExecuted(ResourceExecutedContext context)
		{
		}
	}
}
