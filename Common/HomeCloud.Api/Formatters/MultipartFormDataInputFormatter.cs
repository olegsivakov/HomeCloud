namespace HomeCloud.Api.Formatters
{
	#region Usings

	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;

	using HomeCloudApi.Helpers;

	using Microsoft.AspNetCore.Http.Features;
	using Microsoft.AspNetCore.Mvc.Formatters;
	using Microsoft.AspNetCore.WebUtilities;

	using Microsoft.Net.Http.Headers;

	#endregion

	/// <summary>
	/// Represents the <see cref="MultipartFormDataInputFormatter"/> class to handle <see cref="multipart/form-data"/> requests.
	/// </summary>
	public class MultipartFormDataInputFormatter : InputFormatter
	{
		#region Private Constants

		/// <summary>
		/// The <see cref="multipart/form-data"/> content type
		/// </summary>
		private const string MultipartContentType = "multipart/form-data";

		/// <summary>
		/// The <see cref="FileName"/> property name
		/// </summary>
		private const string FileNamePropertyName = "FileName";

		/// <summary>
		/// The <see cref="Stream"/> property name
		/// </summary>
		private const string StreamPropertyName = "Stream";

		#endregion

		#region Private Members

		/// <summary>
		/// The default form options to set the default limits for request body data.
		/// </summary>
		private static readonly FormOptions DefaultFormOptions = new FormOptions();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MultipartFormDataInputFormatter"/> class.
		/// </summary>
		public MultipartFormDataInputFormatter()
			: base()
		{
			this.SupportedMediaTypes.Add(MultipartContentType);
		}

		#endregion

		/// <summary>
		/// Indicates whether the current instance of <see cref="InputFormatter"/> can read request.
		/// </summary>
		/// <param name="context"><see cref="InputFormatterContext"/> context.</param>
		/// <returns><c>true</c> if the instance can read request. Otherwise <c>false</c>.</returns>
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
		[SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "C# 7.0 syntax not supported")]
		public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
		{
			if (context is null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			try
			{
				var request = context.HttpContext.Request;

				if (request.Body.CanSeek)
				{
					request.Body.Seek(0, SeekOrigin.Begin);
				}

				object model = context.ModelType.GetConstructor(Type.EmptyTypes).Invoke(null);

				string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(request.ContentType), DefaultFormOptions.MultipartBoundaryLengthLimit);
				MultipartReader reader = new MultipartReader(boundary, request.Body);

				bool isSuccess = false;
				bool isReadable = true;

				do
				{
					MultipartSection section = await reader.ReadNextSectionAsync();

					isReadable = section != null;
					if (isReadable && ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition))
					{
						if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
						{
							SetProperty(model, FileNamePropertyName, HeaderUtilities.RemoveQuotes(contentDisposition.FileName).ToString());
							SetProperty(model, StreamPropertyName, section.Body, false);

							isSuccess = true;

							isReadable = false;
						}
						else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
						{
							string key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).ToString();
							Encoding encoding = GetEncoding(section);

							string value = null;

							using (StreamReader streamReader = new StreamReader(section.Body, encoding, detectEncodingFromByteOrderMarks: true, bufferSize: 1024, leaveOpen: true))
							{
								value = await streamReader.ReadToEndAsync();

								if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
								{
									value = string.Empty;
								}
							}

							SetProperty(model, key, value);

							isSuccess = true;
						}
					}
				}
				while (isReadable);

				if (!isSuccess && !context.TreatEmptyInputAsDefaultValue)
				{
					return InputFormatterResult.NoValue();
				}

				return InputFormatterResult.Success(model);
			}
			catch
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
		[SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "C# 7.0 syntax not supported")]
		private static Encoding GetEncoding(MultipartSection section)
		{
			if (!MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue mediaType) || Encoding.UTF7.Equals(mediaType.Encoding))
			{
				return Encoding.UTF8;
			}

			return mediaType.Encoding;
		}

		/// <summary>
		/// Sets the specified value to the object property by property name.
		/// </summary>
		/// <param name="model">The object model.</param>
		/// <param name="propertyName">Property name.</param>
		/// <param name="value">The value.</param>
		/// <param name="convert">Sets the value indicating whether the conversion to the object property type should be done.</param>
		private static void SetProperty(object model, string propertyName, object value, bool convert = true)
		{
			PropertyInfo property = model.GetType().GetProperties().FirstOrDefault(field => field.Name.ToLower() == propertyName?.ToLower());
			if (model != null && property != null)
			{
				try
				{
					if (convert)
					{
						value = Convert.ChangeType(value, property.PropertyType);
					}

					property.SetValue(model, value, null);
				}
				catch
				{
				}
			}
		}

		#endregion
	}
}
