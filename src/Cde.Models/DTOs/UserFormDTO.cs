using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cde.Models.DTOs
{
	public class UserFormDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Role { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
