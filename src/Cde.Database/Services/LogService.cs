using Cde.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cde.Database.Services
{
	public class LogService : DatabaseService<LogModel>
	{
		public LogService(ApplicationContext context) : base(context) { }

		public int CountEvents(int systemId, Level level, Branch branch) {
			return _context.Log.Where(l => l.Level == level && l.SystemId == systemId && l.Branch == branch).Count();
		}

		public IQueryable<LogModel> GetAll(int systemId, Branch branch) {
			return _context.Log.Where(l => l.SystemId == systemId && l.Branch == branch);
		}

		public IQueryable<LogModel> GetById(int id) {
			return from log in _context.Log
				   join system in _context.Service on log.SystemId equals system.Id
				   where log.Id == id
				   select new LogModel() {
					   Id = log.Id,
					   Title = log.Title,
					   Details = log.Details,
					   Level = log.Level,
					   Branch = log.Branch,
					   Date = log.Date,
					   SystemId = log.SystemId,
					   System = system
					};
		}

		public IQueryable<LogModel> GetBySystemId(int id) {
			return from log in _context.Log
				   join system in _context.Service on log.SystemId equals system.Id
				   where system.Id == id
				   select new LogModel() {
					   Id = log.Id,
					   Title = log.Title,
					   Details = log.Details,
					   Level = log.Level,
					   Branch = log.Branch,
					   Date = log.Date,
					   SystemId = log.SystemId,
					   System = system
				   };
		}

		public IQueryable<LogModel> GetByBranch(int systemId, Branch brach) {
			return from log in _context.Log
				   join system in _context.Service on log.SystemId equals system.Id
				   where log.Branch == brach && system.Id == systemId
				   select new LogModel() {
					   Id = log.Id,
					   Title = log.Title,
					   Details = log.Details,
					   Level = log.Level,
					   Branch = log.Branch,
					   Date = log.Date,
					   SystemId = log.SystemId,
					   System = system
				   };
		}

		public IQueryable<LogModel> GetByLevel(int systemId, Branch Branch, Level level) {
			return from log in _context.Log
				   join system in _context.Service on log.SystemId equals system.Id
				   where log.Level == level && system.Id == systemId && log.Branch == Branch
				   select new LogModel() {
					   Id = log.Id,
					   Title = log.Title,
					   Details = log.Details,
					   Level = log.Level,
					   Branch = log.Branch,
					   Date = log.Date,
					   SystemId = log.SystemId,
					   System = system
				   };
		}
	}
}
