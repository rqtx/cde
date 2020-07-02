using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cde.Database.Maps
{
	class BranchMap
	{
		public BranchMap(EntityTypeBuilder<BranchModel> entityBuilder) {
			entityBuilder.HasKey(b => b.Id);
			entityBuilder.ToTable("branch");

			entityBuilder.Property(b => b.Id).HasColumnName("id");
			entityBuilder.Property(b => b.Branch).HasColumnName("name");

			entityBuilder.Property(b => b.Branch).HasConversion(b => b.ToString(), b => (Branch)Enum.Parse(typeof(Branch), b));
		}
	}
}
