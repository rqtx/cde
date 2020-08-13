using Cde.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.IServices
{
	public interface ILogService : IDatabaseService<LogModel>
	{
		/**
		 * <summary> Count events at a log level on a system </summary>
		 * <param name="systemId"> System id </param>
		 * <param name="levelId"> Level id </param>
		 * <returns> Number of occurrences of a specific level in the system </returns>
		 * **/
		public int CountEvents(int systemId, int levelId);

		/**
		 * <summary> Get a log page with size of <paramref name="pageSize"/> by <paramref name="systemId"/></summary>
		 * <param name="systemId"> System id </param>
		 * <param name="pageSize"> Size of the page </param>
		 * <param name="page"> Number od the page </param>
		 * <param name="sortby"> Sort argument, can be title, date or level </param>
		 * <param name="orderby"> Order argument, asc to ascending and desc to descending order </param>
		 * <returns> IList of LogModel </returns>
		 * **/
		public IList<LogModel> GetPageBySystemId(int systemId, byte pageSize, int? page = null, string sortby = null, string orderby = "asc");

		/**
		 * <summary> Get a page log by system and level </summary>
		 * <param name="systemId"> System id </param>
		 * <param name="levelId"> Level id </param>
		 * <param name="pageSize"> Size of the page </param>
		 * <param name="page"> Number od the page </param>
		 * <param name="sortby"> Sort argument, can be title, date or level </param>
		 * <param name="orderby"> Order argument, asc to ascending and desc to descending order </param>
		 * <returns> IList of LogModel </returns>
		 * **/
		public IList<LogModel> GetPageBySystemAndLevel(int systemId, int levelId, byte pageSize, int? page = null, string sortby = null, string orderby = "asc");

		/**
		 * <summary> Get a scpecific log or null if empty</summary>
		 * <param name="id"> System id </param>
		 * <returns> LogModel </returns>
		 * **/
		public LogModel GetById(int id);

		/**
		 * <summary> Get the most recent system logs by level </summary>
		 * <param name="systemId"> System id </param>
		 * <param name="levelId"> Level id </param>
		 * <returns> LogModel </returns>
		 * **/
		public LogModel GetRecentByLevel(int systemId, int levelId);

		public void DeleteByLevel(int systemId, int levelId);

		public void DeleteByDate(int systemId, DateTime date);
	}
}
