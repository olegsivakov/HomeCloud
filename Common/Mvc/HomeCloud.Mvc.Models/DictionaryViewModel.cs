namespace HomeCloud.Mvc.Models
{
	#region Usings

	using System;
	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// Represents the view model of dictionary item.
	/// </summary>
	public class DictionaryViewModel
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryViewModel"/> class.
		/// </summary>
		/// <param name="pair">The key-value pair.</param>
		public DictionaryViewModel(KeyValuePair<Guid, string> pair)
		{
			this.ID = pair.Key;
			this.Name = pair.Value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryViewModel"/> class.
		/// </summary>
		/// <param name="pair">The key-value pair.</param>
		public DictionaryViewModel(KeyValuePair<string, string> pair)
		{
			this.ID = pair.Key;
			this.Name = pair.Value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DictionaryViewModel"/> class.
		/// </summary>
		/// <param name="pair">The key-value pair.</param>
		public DictionaryViewModel(KeyValuePair<int, string> pair)
		{
			this.ID = pair.Key;
			this.Name = pair.Value;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public object ID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		#endregion
	}
}
