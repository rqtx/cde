using Cde.Database.IServices;
using Cde.Database.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database
{
	public static class DbDependenceInjector
	{
		public static void RegisterScoped(IServiceCollection services) {
			services.AddScoped(typeof(IDatabaseService<>), typeof(DatabaseService<>));
			services.AddScoped<ILevelService, LevelService>();
			services.AddScoped<ILogService, LogService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<ISystemService, SystemService>();
			services.AddScoped<IUserService, UserService>();
		}
	}
}
