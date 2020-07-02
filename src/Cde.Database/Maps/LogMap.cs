using System;
using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cde.Database.Maps
{
	class LogMap
	{
		public LogMap(EntityTypeBuilder<LogModel> entityBuilder) {
			entityBuilder.HasKey(l => l.Id);
			entityBuilder.ToTable("log");

			entityBuilder.Property(l => l.Id).HasColumnName("id").IsRequired();
			entityBuilder.Property(l => l.Title).HasColumnName("title").IsRequired();
			entityBuilder.Property(l => l.Details).HasColumnName("details").IsRequired();
			entityBuilder.Property(l => l.Level).HasColumnName("level").IsRequired();
			entityBuilder.Property(l => l.Branch).HasColumnName("branch").IsRequired();
			entityBuilder.Property(l => l.Date).HasColumnName("date").IsRequired();
			entityBuilder.Property(l => l.SystemId).HasColumnName("systemid").IsRequired();

			entityBuilder.HasOne(l => l.System).WithMany(s => s.Logs);
			entityBuilder.HasOne(l => l.BranchRelation).WithMany();
			entityBuilder.HasOne(l => l.LevelRelation).WithMany();

			entityBuilder.Property(l => l.Branch).HasConversion(b => b.ToString(), b => (Branch)Enum.Parse(typeof(Branch), b));
		}
	}
}
