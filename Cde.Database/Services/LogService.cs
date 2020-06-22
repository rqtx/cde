using Cde.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cde.Database.Services
{
	public class LogService : DatabaseService<LogModel>
	{
		public LogService(ApplicationContext context) : base(context) { }
		public override async Task<LogModel> GetById(int id) {
			return await _context.Set<LogModel>().FirstOrDefaultAsync(e => e.Id == id);
		}
	}
}
