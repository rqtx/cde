using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models
{
	public class LogDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Level Level { get; set; }
		public int Events { get; set; }
	}
}
