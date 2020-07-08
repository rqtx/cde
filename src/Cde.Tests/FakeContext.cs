using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cde.Database;
using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cde.Tests
{
	public class FakeContext
	{
		public DbContextOptions<ApplicationDbContext> FakeOptions { get; }

		private Dictionary<Type, string> DataFileNames { get; } = new Dictionary<Type, string>();

		private string FileName<T>() { return DataFileNames[typeof(T)]; }

		public FakeContext() {
			FakeOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

            DataFileNames.Add(typeof(UserModel), $"FakeData{Path.DirectorySeparatorChar}users.json");
            DataFileNames.Add(typeof(LevelModel), $"FakeData{Path.DirectorySeparatorChar}levels.json");
            DataFileNames.Add(typeof(SystemModel), $"FakeData{Path.DirectorySeparatorChar}systems.json");
            DataFileNames.Add(typeof(LogModel), $"FakeData{Path.DirectorySeparatorChar}logs.json");
        }

        public void FillWithAll() {
            FillWith<UserModel>();
            FillWith<LevelModel>();
            FillWith<SystemModel>();
            FillWith<LogModel>();
        }

        public void FillWith<T>() where T : class {
            using (var context = new ApplicationDbContext(FakeOptions)) {
                if (context.Set<T>().Count() == 0) {
                    foreach (T item in GetFakeData<T>())
                        context.Set<T>().Add(item);
                    context.SaveChanges();
                }
            }
        }

        public List<T> GetFakeData<T>() {
            string content = File.ReadAllText(FileName<T>());
            return JsonConvert.DeserializeObject<List<T>>(content);
        }
    }
}
