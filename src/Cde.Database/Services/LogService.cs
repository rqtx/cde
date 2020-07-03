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
		public LogService(ApplicationContext context) : base(context) { }

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

		/**
		 * <summary> Get a scpecific log
		 * <param name="id"> System id
		 * <returns> IQueryable
		 * **/
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

		/**
		 * <summary> Get all system logs by level
		 * <param name="systemId"> System id
		 * <param name="levelId"> Level id
		 * <returns> IQueryable
		 * **/
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

		/**
		 * <summary> Get the most recent system logs by level
		 * <param name="systemId"> System id
		 * <param name="levelId"> Level id
		 * <returns> IQueryable
		 * **/
		public IQueryable<LogModel> GetRecentByLevel(int systemId, int levelId) {
			return (from log in _context.Log
					join system in _context.Service on log.SystemId equals system.Id
					join level in _context.Level on log.LevelId equals level.Id
					where log.LevelId == levelId && system.Id == systemId
					orderby log.Date descending
					select new LogModel() {
						Id = log.Id,
						Title = log.Title,
						Details = log.Details,
						LevelId = log.LevelId,
						Date = log.Date,
						SystemId = log.SystemId,
						System = system,
						Level = level
					}).Take(1);
		}
	}
}
