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
	/**
	 * <summary> A generic class that makes simple DB querys.
	 * <para> If is necessery to do more complex DB querys you should construct
	 * a new Sevice Class that inherit this class. All its methods can be overridden
	 * **/
	public class DatabaseService<T> where T : class
	{
		protected readonly ApplicationDbContext _context;

		public DatabaseService(ApplicationDbContext context) {
			_context = context;
		}

		/**
		 * <summary> Return all <T> elements
		 * <returns> IQueryable
		 * **/
		public virtual IQueryable<T> GetAll() {
			return _context.Set<T>();
		}

		/**
		 * <summary> Return <T> elements
		 * <param name="e"> Expression to use
		 * <returns> IQueryable
		 * **/
		public virtual IQueryable<T> Get(Expression<Func<T, bool>> e) {
			return _context.Set<T>().Where(e);
		}

		/**
		 * <summary> Create an <T> element 
		 * <param name="entity"> Element to be created
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
		 * <summary> Update an <T> element 
		 * <param name="entity"> Element to be updated
		 * **/
		public virtual T Update(T entity) {
			_context.Set<T>().Update(entity);
			_context.SaveChanges();
			return entity;
		}

		/**
		 * <summary> Delete an <T> element 
		 * <param name="entity"> Element to be deleted
		 * **/
		public virtual void Delete(T entity) {
			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
		}
	}
}
