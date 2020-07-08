using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cde.Models
{
	public class SystemModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<LogModel> Logs { get; set; }
	}
}
