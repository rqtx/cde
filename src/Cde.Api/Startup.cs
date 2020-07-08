using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Cde.Database;
using Cde.Middlewares;

namespace Cde
{
	public class Startup
	{
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();

			// https://docs.huihoo.com/ndoc/npgsql/Npgsql.NpgsqlConnectionStringBuilderMembers.html
			var builder = new NpgsqlConnectionStringBuilder() {
				Host = "localhost",
				Port = 5432,
				Database = "cde",
				Username = "postgres",
				Password = "example",
				SslMode = SslMode.Prefer
			};
			services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(optionsAction: opt => opt.UseNpgsql(builder.ConnectionString));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.ConfigureCustomExceptionMiddleware();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
