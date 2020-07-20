using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Cde.Models
{
	public class SystemModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		public IEnumerable<LogModel> Logs { get; set; }
	}
}
