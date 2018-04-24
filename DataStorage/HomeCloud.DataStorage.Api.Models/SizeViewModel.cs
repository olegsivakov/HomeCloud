namespace HomeCloud.DataStorage.Api.Models
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the storage size view model
	/// </summary>
	public class SizeViewModel
	{
		#region Constants

		/// <summary>
		/// The size names
		/// </summary>
		private static readonly IList<string> SizeNames = new List<string>() { "bytes", "KB", "MB", "GB", "TB" };

		/// <summary>
		/// The byte devider, i.e. 1024.
		/// </summary>
		private const int ByteDevider = 1024;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SizeViewModel"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public SizeViewModel(long value)
		{
			this.Value = value;
			this.Text = this.Round((float)this.Value);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the size value.
		/// </summary>
		/// <value>
		/// The size value.
		/// </value>
		public long Value { get; set; } = 0;

		/// <summary>
		/// Gets or sets the size text.
		/// </summary>
		/// <value>
		/// The size text.
		/// </value>
		public string Text { get; set; }

		#endregion

		#region Private Methods

		/// <summary>
		/// Rounds the specified value and returns the stringified size value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="depth">The depth.</param>
		/// <returns>The stringified size value.</returns>
		private string Round(float value, int depth = 0)
		{
			if (value < 800 || depth == SizeNames.Count - 1)
			{
				float floating = value % 1;
				if (floating < 0.1)
				{
					value -= floating;
				}

				return string.Format($"{value} {SizeNames[depth]}");
			}
			else
			{
				value = (float)Math.Round((double)value / ByteDevider, 2);

				return this.Round(value, ++depth);
			}
		}

		#endregion
	}
}
