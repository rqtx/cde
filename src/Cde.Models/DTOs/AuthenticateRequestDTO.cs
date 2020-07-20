using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cde.Models.DTOs
{
	public class AuthenticateRequestDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
