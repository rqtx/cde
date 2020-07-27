using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cde.Database.IServices
{
	public interface IDatabaseService<T> : IDisposable where T : class
	{
		public IList<T> GetAll();
		public IList<T> Get(Expression<Func<T, bool>> e);
		public T Create(T entity);
		public T Update(T entity);
		public void Delete(T entity);
	}
}
