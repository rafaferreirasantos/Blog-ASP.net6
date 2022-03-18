using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            builder.Property(x => x.Name)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);
            builder.Property(x => x.Bio);
            builder.Property(x => x.Email);
            builder.Property(x => x.Image);
            builder.Property(x => x.PasswordHash);
            builder.HasIndex(x => x.Id, "IX_User_Slug")
                .IsUnique();

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<String, object>>(
                    "UserRole",
                    user => user.HasOne<Role>()
                                .WithMany()
                                .HasForeignKey("RoleId")
                                .HasConstraintName("FK_UserRole_RoleId")
                                .OnDelete(DeleteBehavior.Cascade),
                    role => role.HasOne<User>()
                                .WithMany()
                                .HasForeignKey("UserId")
                                .HasConstraintName("FK_UserRole_UserId")
                                .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}