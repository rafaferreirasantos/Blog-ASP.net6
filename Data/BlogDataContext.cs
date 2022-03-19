using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
  public class BlogDataContext : DbContext
  {
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<PostWithTagCount>? PostsWithTagCount { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=Blog;User Id=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new CategoryMap());
      modelBuilder.ApplyConfiguration(new PostMap());
      modelBuilder.ApplyConfiguration(new RoleMap());
      modelBuilder.ApplyConfiguration(new TagMap());
      modelBuilder.ApplyConfiguration(new UserMap());

      modelBuilder
          .Entity<PostWithTagCount>(x =>
          {
            x.HasNoKey();
            x.ToSqlQuery(@"
                    SELECT [Title]
                        , (SELECT COUNT([Id]) FROM [Tag] WHERE [Tag].[Id]=[Post].[Id]) AS [TagCount]
                    FROM [Post]");
          });
    }
  }
}