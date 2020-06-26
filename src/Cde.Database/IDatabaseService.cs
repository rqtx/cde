using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Cde.Database
{
	public interface IDatabaseService<T>
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        Task Create(T entity);

        Task Update(int id, T entity);

        Task Delete(int id);
    }
}
