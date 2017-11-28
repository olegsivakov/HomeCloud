using HomeCloudApi.Helpers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Api.Formatters
{
	/// <summary>
	/// Represents the <see cref="MediaTypeFormatter"/> class to handle multipart/form-data. 
	/// </summary>
	public class MultipartFormDataInputFormatter : InputFormatter
	{
		#region Private Members

		/// <summary>
		/// The default form options to set the default limits for request body data.
		/// </summary>
		private static readonly FormOptions DefaultFormOptions = new FormOptions();

		#endregion

		/// <summary>
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		/// <inheritdoc />
		public override bool CanRead(InputFormatterContext context)
		{
			return MultipartRequestHelper.IsMultipartContentType(context.HttpContext.Request.ContentType);
		}

		/// <summary>
		/// Reads an object from the request body.
		/// </summary>
		/// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Formatters.InputFormatterContext" />.</param>
		/// <returns>
		/// A <see cref="T:System.Threading.Tasks.Task" /> that on completion deserializes the request body.
		/// </returns>
		public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			try
			{
				var request = context.HttpContext.Request;

				if (!request.Body.CanSeek)
				{
					request.Body.Seek(0L, SeekOrigin.Begin);
				}

				Dictionary<string, object> formData = new Dictionary<string, object>();

				string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
				MultipartReader reader = new MultipartReader(boundary, request.Body);

				MultipartSection section = await reader.ReadNextSectionAsync();
				if (section != null)
				{
					if (ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition))
					{
						if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
						{
							formData.Add("FileName", HeaderUtilities.RemoveQuotes(contentDisposition.FileName).ToString());
							formData.Add("Stream", section.Body);
						}

						else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
						{
							string key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).ToString();
							Encoding encoding = GetEncoding(section);

							using (StreamReader streamReader = new StreamReader(section.Body, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
							{
								string value = await streamReader.ReadToEndAsync();

								if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
								{
									value = string.Empty;
								}

								formData[key] = value;

								if (formData.Count > DefaultFormOptions.ValueCountLimit)
								{
									throw new InvalidDataException($"Form key count limit {DefaultFormOptions.ValueCountLimit} exceeded.");
								}
							}
						}
					}
				}

				if (!formData.Any() && !context.TreatEmptyInputAsDefaultValue)
				{
					return InputFormatterResult.NoValue();
				}

				Type modelType = context.ModelType;
				object model = context.ModelType.GetConstructor(Type.EmptyTypes).Invoke(null);

				foreach (KeyValuePair<string, object> item in formData)
				{
					PropertyInfo property = modelType.GetProperty(item.Key, BindingFlags.IgnoreCase);
					if (property != null)
					{
						try
						{
							object value = Convert.ChangeType(item.Value, property.PropertyType);
							property.SetValue(value, model);
						}
						catch (Exception exception)
						{
						}
					}
				}

				return InputFormatterResult.Success(model);
			}
			catch(Exception exception)
			{
				return InputFormatterResult.Failure();
			}
		}

		#region Private Methods

		/// <summary>
		/// Gets the encoding of the specified multipart section.
		/// </summary>
		/// <param name="section">The multipart section of <see cref="MultipartSection"/> type.</param>
		/// <returns>The encoding presented by th einstance of <see cref="Encoding"/>.</returns>
		private static Encoding GetEncoding(MultipartSection section)
		{
			if (!MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue mediaType) || Encoding.UTF7.Equals(mediaType.Encoding))
			{
				return Encoding.UTF8;
			}

			return mediaType.Encoding;
		}

		#endregion
	}
}
