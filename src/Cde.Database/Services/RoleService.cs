using Cde.Database.IServices;
using Cde.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Services
{
	public class RoleService : DatabaseService<RoleModel>, IRoleService
	{
		public RoleService(ApplicationDbContext context) : base(context) { }
	}
}
