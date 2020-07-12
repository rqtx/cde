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
		 * <summary> Count events at a log level on a system
		 * <param name="systemId"> System id
		 * <param name="levelId"> Level id
		 * <returns> Number of occurrences of a specific level in the system
		 * **/
		public int CountEvents(int systemId, int levelId) {
			return _context.Log.Where(l => l.LevelId == levelId && l.SystemId == systemId).Count();
		}

		/**
		 * <summary> Get all logs of a system
		 * <param name="systemId"> System id
		 * <returns> IQueryable
		 * **/
		public IQueryable<LogModel> GetAllBySystemId(int systemId) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.SystemId == systemId);
		}

		/**
		 * <summary> Get a scpecific log
		 * <param name="id"> System id
		 * <returns> IQueryable
		 * **/
		public IQueryable<LogModel> GetById(int id) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.Id == id);
		}

		/**
		 * <summary> Get all system logs by level
		 * <param name="systemId"> System id
		 * <param name="levelId"> Level id
		 * <returns> IQueryable
		 * **/
		public IQueryable<LogModel> GetByLevel(int systemId, int levelId) {
			return _context.Log
					.Include(l => l.System)
					.Include(l => l.Level)
					.Where(l => l.SystemId == systemId && l.LevelId == levelId);
		}

		/**
		 * <summary> Get the most recent system logs by level
		 * <param name="systemId"> System id
		 * <param name="levelId"> Level id
		 * <returns> IQueryable
		 * **/
		public IQueryable<LogModel> GetRecentByLevel(int systemId, int levelId) {
			return (from log in _context.Log
					join system in _context.System on log.SystemId equals system.Id
					join level in _context.Level on log.LevelId equals level.Id
					where log.LevelId == levelId && system.Id == systemId
					orderby log.CreatedAt descending
					select log).Take(1);
		}
	}
}
