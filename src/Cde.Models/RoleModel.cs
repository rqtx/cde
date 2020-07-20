using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Cde.Models
{
	public class RoleModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		public IEnumerable<UserModel> Users { get; set; }
	}
}
