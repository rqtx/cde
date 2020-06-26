using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cde.Models
{
	public class UserModel : IUser
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string Salt { get; set; }
		public string Passhash { get; set; }
	}
}
