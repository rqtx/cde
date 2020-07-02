﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cde.Database.Maps;
using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cde.Database
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

		public DbSet<LogModel> Log { get; set; }
		public DbSet<UserModel> User { get; set; }
		public DbSet<SystemModel> Service { get; set; }
		public DbSet<LevelModel> Level { get; set; }
		public DbSet<BranchModel> Branche { get; set; }

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);

			new LogMap(builder.Entity<LogModel>());
			new UserMap(builder.Entity<UserModel>());
			new SystemMap(builder.Entity<SystemModel>());
			new LevelMap(builder.Entity<LevelModel>());
			new BranchMap(builder.Entity<BranchModel>());
		}
	}
}
