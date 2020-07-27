using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cde.Models;
using System.Linq.Expressions;
using Cde.Database.IServices;

// https://codingblast.com/entity-framework-core-generic-repository/
namespace Cde.Database.Services
{
	/**
	 * <summary> A generic class that makes simple DB querys.
	 * <para> If is necessery to do more complex DB querys you should construct
	 * a new Sevice Class that inherit this class. All its methods can be overridden
	 * **/
	public class DatabaseService<T> : IDatabaseService<T> where T : class
	{
		protected readonly ApplicationDbContext _context;

		public DatabaseService(ApplicationDbContext context) {
			_context = context;
		}

		/**
		 * <summary> Return all <T> elements </summary>
		 * <returns> IList </returns>
		 * **/
		public virtual IList<T> GetAll() {
			return _context.Set<T>().ToList();
		}

		/**
		 * <summary> Return <T> elements </summary>
		 * <param name="e"> Expression to use </param>
		 * <returns> IList </returns>
		 * **/
		public virtual IList<T> Get(Expression<Func<T, bool>> e) {
			return _context.Set<T>().Where(e).ToList();
		}

		/**
		 * <summary> Create an <T> element  </summary>
		 * <param name="entity"> Element to be created </param>
		 * **/
		public virtual T Create(T entity) {
			try {
				_context.Set<T>().Add(entity);
				_context.SaveChanges();
				return entity;
			} catch (DbUpdateException e) {
				_context.Remove(entity);
				throw e;
			}
		}

		/**
		 * <summary> Update an <T> element  </summary>
		 * <param name="entity"> Element to be updated </param>
		 * **/
		public virtual T Update(T entity) {
			_context.Set<T>().Update(entity);
			_context.SaveChanges();
			return entity;
		}

		/**
		 * <summary> Delete an <T> element  </summary>
		 * <param name="entity"> Element to be deleted </param>
		 * **/
		public virtual void Delete(T entity) {
			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
		}

		public void Dispose() {
			if (null != _context) {
				_context.Dispose();
			}
		}
	}
}
