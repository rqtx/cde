using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cde.Database
{
	public abstract class DatabaseService<T> where T : class, new()
	{
		protected readonly ApplicationContext _context;

		public DatabaseService(ApplicationContext context) {
			_context = context;
		}

		public virtual IQueryable<T> GetAll() {
			return _context.Set<T>();
		}

		public abstract Task<T> GetById(int id);

		public virtual void Create(T entity) {
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
		}

		public virtual async Task Update(int id, T entity) {
			_context.Set<T>().Update(entity);
			await _context.SaveChangesAsync();
		}
		public virtual async Task Delete(int id) {
			var entity = await GetById(id);
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
