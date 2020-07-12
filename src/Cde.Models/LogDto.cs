using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models
{
	public class LogDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Details { get; set; }
		public DateTime CreatedAt { get; set; }
		public string SystemName { get; set; }
		public string LevelName { get; set; }

		public LogDto(LogModel log) {
			Id = log.Id;
			Title = log.Title;
			Details = log.Details;
			CreatedAt = log.CreatedAt;
			SystemName = log.System.Name;
			LevelName = log.Level.Name;
		}
	}
}
