using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cde.Models.DTOs
{
	public class UserDTO
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string RoleName { get; set; }

	}
}
