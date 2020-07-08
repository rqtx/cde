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

			entityBuilder.Property(l => l.Id).HasColumnName("id");
			entityBuilder.Property(l => l.Title).HasColumnName("title").IsRequired();
			entityBuilder.Property(l => l.Details).HasColumnName("details").IsRequired();
			entityBuilder.Property(l => l.CreatedAt).HasColumnName("created_at").IsRequired();
			entityBuilder.Property(l => l.SystemId).HasColumnName("systemid").IsRequired();
			entityBuilder.Property(l => l.LevelId).HasColumnName("levelid").IsRequired();

			entityBuilder.HasOne(l => l.System).WithMany(s => s.Logs);
			entityBuilder.HasOne(l => l.Level).WithMany();

		}
	}
}
