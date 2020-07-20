using Cde.Database.IServices;
using Cde.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Services
{
	public class LevelService : DatabaseService<LevelModel>, ILevelService
	{
		public LevelService(ApplicationDbContext context) : base(context) { }
	}
}
