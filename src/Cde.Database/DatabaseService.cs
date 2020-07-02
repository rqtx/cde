using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cde.Models;
using System.Linq.Expressions;

// https://codingblast.com/entity-framework-core-generic-repository/
namespace Cde.Database
{
	public class DatabaseService<T> where T : class
	{
		protected readonly ApplicationContext _context;

		public DatabaseService(ApplicationContext context) {
			_context = context;
		}

		public virtual IQueryable<T> GetAll() {
			return _context.Set<T>();
		}

		public virtual List<T> Get(Expression<Func<T, bool>> e) {
			return _context.Set<T>().Where(e).ToList();
		}

		public virtual void Create(T entity) {
			try {
				_context.Set<T>().Add(entity);
				_context.SaveChanges();
			} catch (DbUpdateException e) {
				_context.Remove(entity);
				throw e;
			}
		}

		public virtual void Update(T entity) {
			_context.Set<T>().Update(entity);
			_context.SaveChanges();
		}
		public virtual void Delete(T entity) {
			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
		}
	}
}
