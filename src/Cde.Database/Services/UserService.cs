using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Cde.Database.Services
{
	public class UserService : DatabaseService<UserModel>
	{
		public UserService(ApplicationContext context) : base(context) { }

		public UserModel GetUserByEmail(string email) {
			return _context.User.Where(u => u.Email == email).First();
		}

	}
}
