namespace HomeCloud.Mvc.Hypermedia
{
	#region Usings

	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using HomeCloud.Mvc.Hypermedia.Relations;

	#endregion

	/// <summary>
	/// Converts <see cref="HypermediaResponse"/> object containing <see cref="HATEOAS"/> links to <see cref="Hypertext Application Language"/> response.
	/// </summary>
	/// <seealso cref="Newtonsoft.Json.JsonConverter" />
	public class HypermediaJsonConverter : JsonConverter
	{
		#region JsonConverter Implementations

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
		/// </summary>
		/// <value>
		///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
		/// </value>
		public override bool CanRead => false;

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType.Equals(typeof(HypermediaResponse));
		}

		/// <summary>
		/// Reads the <see cref="JSON"/> representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>
		/// The object value.
		/// </returns>
		/// <exception cref="NotSupportedException">The method is not supported.</exception>
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Writes the <see cref="JSON"/> representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			HypermediaResponse model = value as HypermediaResponse;
			if (model != null && model.Data != null)
			{
				JToken token = JToken.FromObject(model.Data, serializer);
				JObject result = model.Data is IEnumerable ? new JObject(new JProperty("items", (JArray)token)) : (JObject)token;

				JObject links = CreateRelationProperties(model.Relations.Where(item => !string.IsNullOrWhiteSpace(item.Name)));
				if (links != null)
				{
					JProperty linksProperty = links is null ? null : new JProperty("_links", links);
					result.Add(linksProperty);
				}

				result.WriteTo(writer);
			}
			else
			{
				JToken token = JToken.FromObject(value);
				token.WriteTo(writer);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Creates the <see cref="JSON"/> object from the specified relations.
		/// </summary>
		/// <param name="relations">The collection of links of <see cref="Relation"/> type.</param>
		/// <returns>the instance of <see cref="JObject"/>.</returns>
		private static JObject CreateRelationProperties(IEnumerable<IRelation> relations)
		{
			if (relations != null && relations.Any())
			{
				JObject result = new JObject();

				foreach(IRelation relation in relations)
				{
					object link = null;
					if (relation is Relation)
					{
						link = JObject.FromObject((relation as Relation).Link);
					}
					else if (relation is RelationList)
					{
						link = JArray.FromObject((relation as RelationList).Links);
					}

					if (link != null)
					{
						result.Add(new JProperty(relation.Name, link));
					}
				}

				return result;
			}

			return null;
		}

		#endregion
	}
}
