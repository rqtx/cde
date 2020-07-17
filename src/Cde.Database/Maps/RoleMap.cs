using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Maps
{
	public class RoleMap
	{
		public RoleMap(EntityTypeBuilder<RoleModel> entityBuilder) {
			entityBuilder.HasKey(l => l.Id);
			entityBuilder.ToTable("role");

			entityBuilder.Property(l => l.Id).HasColumnName("id");
			entityBuilder.Property(l => l.Name).HasMaxLength(25).HasColumnName("name");

			entityBuilder.HasMany(u => u.Users).WithOne(r => r.Role);
		}
	}
}
