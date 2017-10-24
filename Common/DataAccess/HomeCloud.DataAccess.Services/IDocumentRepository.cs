﻿namespace HomeCloud.DataAccess.Services
{
	#region Usings

	using System;
	using System.Collections.Generic;

	using HomeCloud.DataAccess.Contracts;

	#endregion

	/// <summary>
	/// Marks the repository to query documents from database.
	/// </summary>
	public interface IDocumentRepository
	{
	}

	/// <summary>
	/// Defines combination of methods against document entity of <see cref="IDocument"/> type.
	/// </summary>
	/// <typeparam name="T">The type of entity.</typeparam>
	public interface IDocumentRepository<T> : IDocumentRepository
		where T : IDocument
	{
		/// <summary>
		/// Gets the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The instance of <see cref="T"/> type.</returns>
		T Get(Guid id);

		/// <summary>
		/// Looks for all records of <see cref="T" /> type.
		/// </summary>
		/// <param name="offset">The offset index.</param>
		/// <param name="limit">The number of records to return.</param>
		/// <returns>
		/// The list of instances of <see cref="T" /> type.
		/// </returns>
		IEnumerable<T> Find(int offset = 0, int limit = 20);

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Save(T entity);

		/// <summary>
		/// Deletes the entity by specified unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier..</param>
		void Delete(Guid id);
	}
}