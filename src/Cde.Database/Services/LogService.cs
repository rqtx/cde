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
		
	}
}
