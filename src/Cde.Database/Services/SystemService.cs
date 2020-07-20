using Cde.Database.IServices;
using Cde.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Services
{
	public class SystemService : DatabaseService<SystemModel>, ISystemService
	{
		public SystemService(ApplicationDbContext context) : base(context) { }
	}
}
