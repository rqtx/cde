using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cde.Models;

// https://codingblast.com/entity-framework-core-generic-repository/
namespace Cde.Database
{
	public class DatabaseService<T> where T : class, IModel, new()
	{
		protected readonly ApplicationContext _context;

		public DatabaseService(ApplicationContext context) {
			_context = context;
		}

		public virtual IQueryable<T> GetAll() {
			return _context.Set<T>();
		}

		public virtual T GetById(int id) {
			return _context.Set<T>().FirstOrDefault(e => e.Id == id);
		}

		public virtual void Create(T entity) {
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
		}

		public virtual void Update(int id, T entity) {
			entity.Id = id;
			_context.Set<T>().Update(entity);
			_context.SaveChanges();
		}
		public virtual void Delete(int id) {
			var entity = GetById(id);
			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
		}
	}
}
