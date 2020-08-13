using Cde.Database.IServices;
using Cde.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cde.Database.Services
{
	/**
	 * <summary> Class to access log table
	 * **/
	public class LogService : DatabaseService<LogModel>, ILogService
	{
		public LogService(ApplicationDbContext context) : base(context) { }

		public int CountEvents(int systemId, int levelId) {
			return _context.Log.Where(l => l.LevelId == levelId && l.SystemId == systemId).Count();
		}

		public IList<LogModel> GetPageBySystemId(int systemId, byte pageSize, int? page = null, string sortby = null, string orderby = "asc") {
			var logs =  _context.Log
						.Include(l => l.System)
						.Include(l => l.Level)
						.Where(l => l.SystemId == systemId)
						.Skip((page ?? 0) * pageSize)
						.Take(pageSize);

			if (null != sortby) 
			{
				switch (sortby) {
					case "title":
						if ("desc" == orderby) {
							logs = logs.OrderByDescending(l => l.Title);
						} else {
							logs = logs = logs.OrderBy(l => l.Title);
						}
						break;
					case "date":
						if ("desc" == orderby) {
							logs = logs.OrderByDescending(l => l.CreatedAt);
						} else {
							logs = logs = logs.OrderBy(l => l.CreatedAt);
						}
						break;
					case "level":
						if ("desc" == orderby) {
							logs = logs.OrderByDescending(l => l.Level.Name);
						} else {
							logs = logs = logs.OrderBy(l => l.Level.Name);
						}
						break;
				}
			}
			return logs.ToList();
		}

		public IList<LogModel> GetPageBySystemAndLevel(int systemId, int levelId, byte pageSize, int? page = null, string sortby = null, string orderby = "asc") {
			var logs = _context.Log
						.Include(l => l.System)
						.Include(l => l.Level)
						.Where(l => l.SystemId == systemId && l.LevelId == levelId)
						.Skip((page ?? 0) * pageSize)
						.Take(pageSize);

			if (null != sortby) {
				switch (sortby) {
					case "title":
						if ("desc" == orderby) {
							logs = logs.OrderByDescending(l => l.Title);
						} else {
							logs = logs = logs.OrderBy(l => l.Title);
						}
						break;
					case "date":
						if ("desc" == orderby) {
							logs = logs.OrderByDescending(l => l.CreatedAt);
						} else {
							logs = logs = logs.OrderBy(l => l.CreatedAt);
						}
						break;
					case "level":
						if ("desc" == orderby) {
							logs = logs.OrderByDescending(l => l.Level.Name);
						} else {
							logs = logs = logs.OrderBy(l => l.Level.Name);
						}
						break;
				}
			}
			return logs.ToList();
		}

		public LogModel GetById(int id) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.Id == id)
					.FirstOrDefault();
		}

		public LogModel GetRecentByLevel(int systemId, int levelId) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.LevelId == levelId && l.SystemId == systemId)
					.OrderByDescending(l => l.CreatedAt)
					.FirstOrDefault();
		}

		public void DeleteByLevel(int systemId, int levelId) {
			var logs = _context.Log.Where(l => l.LevelId == levelId && l.SystemId == systemId);
			if (null != logs) {
				_context.Log.RemoveRange(logs);
				_context.SaveChanges();
			}
		}

		public void DeleteByDate(int systemId, DateTime date) {
			var logs = _context.Log.Where(l => l.CreatedAt <= date  && l.SystemId == systemId);
			if (null != logs) {
				_context.Log.RemoveRange(logs);
				_context.SaveChanges();
			}
		}
	}
}
