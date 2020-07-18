using Cde.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cde.Database.Services
{
	public class UserService : DatabaseService<UserModel>
	{
		public UserService(ApplicationDbContext context) : base(context) { }

		/**
		 * <summary> Get all users </summary>
		 * <returns> IQueryable </returns>
		 * **/
		public override IQueryable<UserModel> GetAll() {
			return _context.User.Include(u => u.Role);
		}

		/**
		 * <summary> Get users according expression </summary>
		 * <returns> IQueryable </returns>
		 * **/
		public override IQueryable<UserModel> Get(Expression<Func<UserModel, bool>> e) {
			return _context.User
					.Include(u => u.Role)
					.Where(e);
		}
	}
}
