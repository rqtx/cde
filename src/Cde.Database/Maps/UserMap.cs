using Cde.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cde.Database.Maps
{
	class UserMap
	{
		public UserMap(EntityTypeBuilder<UserModel> entityBuilder) {
			entityBuilder.HasKey(x => x.Id);
			entityBuilder.ToTable("account");

			entityBuilder.Property(x => x.Id).HasColumnName("id").IsRequired();
			entityBuilder.Property(x => x.Name).HasMaxLength(15).HasColumnName("name").IsRequired();
			entityBuilder.Property(x => x.RoleId).HasColumnName("roleid").IsRequired();
			entityBuilder.Property(x => x.Salt).HasColumnName("salt").IsRequired();
			entityBuilder.Property(x => x.Passhash).HasColumnName("passhash").IsRequired();
			entityBuilder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();

			entityBuilder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
		}
	}
}
