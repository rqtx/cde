using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Maps
{
	class LevelMap
	{
		public LevelMap(EntityTypeBuilder<LevelModel> entityBuilder) {
			entityBuilder.HasKey(l => l.Id);
			entityBuilder.ToTable("level");

			entityBuilder.Property(l => l.Id).HasColumnName("id");
			entityBuilder.Property(l => l.Level).HasColumnName("name");
		}
	}
}
