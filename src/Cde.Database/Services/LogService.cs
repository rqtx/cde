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

		public int CountEvents(int systemId, int levelId) {
			return _context.Log.Where(l => l.LevelId == levelId && l.SystemId == systemId).Count();
			/*return (from log in _context.Log
					join system in _context.Service on log.SystemId equals system.Id
					where log.SystemId == systemId && log.Level == level
					select log).Count();*/
		}

		public IQueryable<LogModel> GetAll(int systemId) {
			return from log in _context.Log
				   join system in _context.Service on log.SystemId equals system.Id
				   join level in _context.Level on log.LevelId equals level.Id
				   where log.SystemId == systemId
				   select new LogModel() {
					   Id = log.Id,
					   Title = log.Title,
					   Details = log.Details,
					   LevelId = log.LevelId,
					   Date = log.Date,
					   SystemId = log.SystemId,
					   System = system,
					   Level = level
				   };
		}

		public IQueryable<LogModel> GetById(int id) {
			return from log in _context.Log
				   join system in _context.Service on log.SystemId equals system.Id
				   join level in _context.Level on log.LevelId equals level.Id
				   where log.Id == id
				   select new LogModel() {
					   Id = log.Id,
					   Title = log.Title,
					   Details = log.Details,
					   LevelId = log.Level.Id,
					   Date = log.Date,
					   SystemId = log.SystemId,
					   System = system,
					   Level = level
				   };
		}

		public IQueryable<LogModel> GetByLevel(int systemId, int levelId) {
			return	from log in _context.Log
					join system in _context.Service on log.SystemId equals system.Id
					join level in _context.Level on log.LevelId equals level.Id
					where log.LevelId == levelId && system.Id == systemId
					select new LogModel() {
						Id = log.Id,
						Title = log.Title,
						Details = log.Details,
						LevelId = log.LevelId,
						Date = log.Date,
						SystemId = log.SystemId,
						System = system,
						Level = level
					};
		}
	}
}
