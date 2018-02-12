using HomeCloud.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HomeCloud.Data.SqlServer
{
	public class SqlServerDBContextScope : ISqlServerDBContextScope
	{
		#region Private Members

		/// <summary>
		/// The database context.
		/// </summary>
		private readonly ISqlServerDBContext context = null;

		/// <summary>
		/// The <see cref="ISqlServerDBRepository"/> factory.
		/// </summary>
		private readonly IServiceFactory<ISqlServerDBRepository> repositoryFactory = null;

		/// <summary>
		/// The database transaction
		/// </summary>
		private bool isTransactionCreated = false;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlServerDBContextScope" /> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="repositoryFactory">The <see cref="ISqlServerDBRepository" /> factory.</param>
		public SqlServerDBContextScope(ISqlServerDBContext context, IServiceFactory<ISqlServerDBRepository> repositoryFactory)
		{
			this.context = context;
			this.repositoryFactory = repositoryFactory;
		}

		#endregion

		#region ISqlServerDBContextScope Implementations

		/// <summary>
		/// Gets the <see cref="ISqlServerDBRepository" /> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="ISqlServerDBRepository" />.</typeparam>
		/// <returns>The instance of <see cref="!:T" />.</returns>
		public T GetRepository<T>() where T : ISqlServerDBRepository
		{
			if (!isTransactionCreated)
			{
				this.context.CreateTransaction();

				isTransactionCreated = true;
			}

			return this.repositoryFactory.GetService<T>();
		}

		/// <summary>
		/// Commits the changes to the database.
		/// </summary>
		public void Commit()
		{
			if (isTransactionCreated)
			{
				this.context.Commit();

				this.isTransactionCreated = false;
			}
		}

		#endregion

		#region IDisposable Implementations

		public void Dispose()
		{
			this.context.Dispose();
		}

		#endregion
	}
}
