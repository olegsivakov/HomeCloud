using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Data.SqlServer
{
    public interface ISqlServerDBContextScope
    {
		/// <summary>
		/// Gets the <see cref="ISqlServerDBRepository<T>"/> repository.
		/// </summary>
		/// <typeparam name="T">The type of the repository derived from <see cref="ISqlServerDBRepository"/>.</typeparam>
		/// <returns>The instance of <see cref="ISqlServerDBRepository"/>.</returns>
		ISqlServerDBRepository<T> GetRepository<T>();
	}
}
