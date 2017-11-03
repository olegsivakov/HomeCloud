namespace HomeCloud.DataStorage.DataAccess.Components.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataStorage.DataAccess.Contracts;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	/// <summary>
	/// Provides combination of methods to handle the document presenting aggregated file.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.IFileAggregationRepository" />
	public class FileAggregationRepository : IFileAggregationRepository
	{
		#region Private Members

		/// <summary>
		/// The document context
		/// </summary>
		private readonly IDocumentContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileAggregationRepository" /> class.
		/// </summary>
		/// <param name="context">The document context.</param>
		public FileAggregationRepository(IDocumentContext context)
		{
			this.context = context;
		}

		#endregion

		#region IAggregatedFileRepository Implementations

		/// <summary>
		/// Deletes the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		public void Delete(Guid id)
		{
			this.context.DeleteAsync<AggregatedFile>(catalog => catalog.ID == id);
		}

		/// <summary>
		/// Looks for all records of <see cref="T" /> type.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="T" /> type.
		/// </returns>
		public IEnumerable<AggregatedFile> Find(int offset = 0, int limit = 20)
		{
			return this.context.FindAsync<AggregatedFile>(catalog => true, offset, limit).Result;
		}

		/// <summary>
		/// Gets the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		public AggregatedFile Get(Guid id)
		{
			return this.context.GetAsync<AggregatedFile>(catalog => catalog.ID == id).Result;
		}

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The instance of <see cref="AggregatedFile"/>.</returns>
		public AggregatedFile Save(AggregatedFile entity)
		{
			return this.context.UpsertAsync(entity).Result;
		}

		#endregion
	}
}
