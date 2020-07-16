using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Maps
{
	public class SystemMap
	{
		public SystemMap(EntityTypeBuilder<SystemModel> entityBuilder) {
			entityBuilder.HasKey(s => s.Id);
			entityBuilder.ToTable("system");

			entityBuilder.Property(s => s.Id).HasColumnName("id").IsRequired();
			entityBuilder.Property(s => s.Name).HasColumnName("name").IsRequired();

			entityBuilder.HasMany(s => s.Logs).WithOne(l => l.System);
		}
	}
}
