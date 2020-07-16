using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cde.Database.Maps;
using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Cde.Database
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<LogModel> Log { get; set; }
		public DbSet<UserModel> User { get; set; }
		public DbSet<SystemModel> System { get; set; }
		public DbSet<LevelModel> Level { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			if (!optionsBuilder.IsConfigured) {
				// https://docs.huihoo.com/ndoc/npgsql/Npgsql.NpgsqlConnectionStringBuilderMembers.html
				var builder = new NpgsqlConnectionStringBuilder() {
					Host = Environment.GetEnvironmentVariable("DB_HOST"),
					Port = int.Parse(Environment.GetEnvironmentVariable("DB_PORT")),
					Database = Environment.GetEnvironmentVariable("DB_NAME"),
					Username = Environment.GetEnvironmentVariable("DB_USER"),
					Password = Environment.GetEnvironmentVariable("DB_PASSWORD"),
					SslMode = SslMode.Prefer
				};
				optionsBuilder.UseNpgsql(builder.ToString());
			}
		}

		protected override void OnModelCreating(ModelBuilder builder) {
			base.OnModelCreating(builder);

			new LogMap(builder.Entity<LogModel>());
			new UserMap(builder.Entity<UserModel>());
			new SystemMap(builder.Entity<SystemModel>());
			new LevelMap(builder.Entity<LevelModel>());
		}
	}
}
