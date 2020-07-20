using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Cde.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int RoleId { get; set; }
		[JsonIgnore]
		public string Salt { get; set; }
		[JsonIgnore]
		public string Passhash { get; set; }
		public DateTime CreatedAt { get; set; }
		public RoleModel Role { get; set; }
	}
}
