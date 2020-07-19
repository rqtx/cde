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
	public class LogService : DatabaseService<LogModel>
	{
		public LogService(ApplicationDbContext context) : base(context) { }

		/**
		 * <summary> Count events at a log level on a system </summary>
		 * <param name="systemId"> System id </param>
		 * <param name="levelId"> Level id </param>
		 * <returns> Number of occurrences of a specific level in the system </returns>
		 * **/
		public int CountEvents(int systemId, int levelId) {
			return _context.Log.Where(l => l.LevelId == levelId && l.SystemId == systemId).Count();
		}

		/**
		 * <summary> Get a log page with size of <paramref name="pageSize"/> by <paramref name="systemId"/></summary>
		 * <param name="systemId"> System id </param>
		 * <returns> IEnumerable of LogModel </returns>
		 * **/
		public IEnumerable<LogModel> GetPageBySystemId(int systemId, byte pageSize, string sortby = null, string orderby = "asc", int? page = null) {
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

		/**
		 * <summary> Get a page log by system and level </summary>
		 * <param name="systemId"> System id </param>
		 * <param name="levelId"> Level id </param>
		 * <returns> IEnumerable of LogModel </returns>
		 * **/
		public IEnumerable<LogModel> GetPageBySystemAndLevel(int systemId, int levelId, byte pageSize, string sortby = null, string orderby = "asc", int? page = null) {
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

		/**
		 * <summary> Get a scpecific log or null if empty</summary>
		 * <param name="id"> System id </param>
		 * <returns> LogModel </returns>
		 * **/
		public LogModel GetById(int id) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.Id == id)
					.FirstOrDefault();
		}

		/**
		 * <summary> Get the most recent system logs by level </summary>
		 * <param name="systemId"> System id </param>
		 * <param name="levelId"> Level id </param>
		 * <returns> LogModel </returns>
		 * **/
		public LogModel GetRecentByLevel(int systemId, int levelId) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.LevelId == levelId && l.SystemId == systemId)
					.OrderByDescending(l => l.CreatedAt)
					.FirstOrDefault();
		}
	}
}
