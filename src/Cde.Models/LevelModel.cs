using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models
{
	public class LevelModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<LogModel> Logs { get; set; }
	}
}
