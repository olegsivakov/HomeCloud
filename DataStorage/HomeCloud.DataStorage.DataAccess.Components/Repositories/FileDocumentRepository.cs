namespace HomeCloud.DataStorage.DataAccess.Components.Repositories
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataStorage.DataAccess.Contracts;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#endregion

	/// <summary>
	/// Provides combination of methods to handle file documents.
	/// </summary>
	/// <seealso cref="HomeCloud.DataStorage.DataAccess.Services.Repositories.IFileDocumentRepository" />
	public class FileDocumentRepository : IFileDocumentRepository
	{
		#region Private Members

		/// <summary>
		/// The document context
		/// </summary>
		private readonly IDocumentContext context = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDocumentRepository" /> class.
		/// </summary>
		/// <param name="context">The document context.</param>
		public FileDocumentRepository(IDocumentContext context)
		{
			this.context = context;
		}

		#endregion

		#region IAggregatedFileRepository Implementations

		/// <summary>
		/// Deletes the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier.</param>
		/// <returns>The asynchronous operation.</returns>
		public async Task DeleteAsync(Guid id)
		{
			await this.context.DeleteAsync<FileDocument>(catalog => catalog.ID == id);
		}

		/// <summary>
		/// Deletes the records of <see cref="FileDocument" /> type by specified expression.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <returns>The asynchronous operation.</returns>
		public async Task DeleteAsync(Expression<Func<FileDocument, bool>> selector)
		{
			await this.context.DeleteAsync(selector);
		}

		/// <summary>
		/// Gets the records of <see cref="FileDocument" /> type by specified expression.
		/// </summary>
		/// <param name="selector">The data expression.</param>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="FileDocument" /> type.
		/// </returns>
		public async Task<IEnumerable<FileDocument>> FindAsync(Expression<Func<FileDocument, bool>> selector, int offset = 0, int limit = 20)
		{
			return await this.context.FindAsync<FileDocument>(selector, offset, limit);
		}

		/// <summary>
		/// Gets the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		public async Task<FileDocument> GetAsync(Guid id)
		{
			return await this.context.GetAsync<FileDocument>(catalog => catalog.ID == id);
		}

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>The instance of <see cref="FileDocument"/>.</returns>
		public async Task<FileDocument> SaveAsync(FileDocument entity)
		{
			return await this.context.UpsertAsync(entity);
		}

		#endregion
	}
}
