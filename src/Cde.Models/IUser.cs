using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Models
{
	public interface IUser : IModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
