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

			entityBuilder.Property(x => x.Id).HasColumnName("id");
			entityBuilder.Property(x => x.Name).HasColumnName("name");
			entityBuilder.Property(x => x.Email).HasColumnName("email");
			entityBuilder.Property(x => x.Salt).HasColumnName("salt");
			entityBuilder.Property(x => x.Passhash).HasColumnName("passhash");
		}
	}
}
