using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cde.Database.Maps
{
	class LogMap
	{
		public LogMap(EntityTypeBuilder<LogModel> entityBuilder) {
			entityBuilder.HasKey(x => x.Id);
			entityBuilder.ToTable("log");

			entityBuilder.Property(x => x.Id).HasColumnName("id");
			entityBuilder.Property(x => x.Msg).HasColumnName("msg");
		}
	}
}
