namespace HomeCloud.DataStorage.DataAccess.Components.Repositories
{
	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Contracts;

	using HomeCloud.DataStorage.DataAccess.Contracts;
	using HomeCloud.DataStorage.DataAccess.Services.Repositories;

	#region Usings

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

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AggregatedFile> Find(int offset = 0, int limit = 20)
		{
			throw new NotImplementedException();
		}

		public AggregatedFile Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Save(AggregatedFile entity)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
