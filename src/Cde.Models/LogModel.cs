using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cde.Models
{
	public class LogModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Details { get; set; }
		public DateTime CreatedAt { get; set; }
		public int SystemId { get; set; }
		public int LevelId { get; set; }
		public SystemModel System { get; set; }
		public LevelModel Level { get; set; }
	}
}
