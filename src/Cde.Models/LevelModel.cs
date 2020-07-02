using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models
{
	public enum Level 
	{ 
		Trace,
		Debug,
		Information,
		Warning,
		Error,
		Critical
	}
	public class LevelModel
	{
		public int Id { get; set; }
		public Level Level { get; set; }
	}
}
