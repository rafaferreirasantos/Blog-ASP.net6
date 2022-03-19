using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
  public class User
  {
    public int Id { get; set; }
    public String Name { get; set; } = "";
    public String Email { get; set; } = "";
    public String PasswordHash { get; set; } = "";
    public String Bio { get; set; } = "";
    public String Image { get; set; } = "";
    public String Slug { get; set; } = "";
    public IList<Post> Posts { get; set; } = new List<Post>();
    public IList<Role> Roles { get; set; } = new List<Role>();

    public override String ToString()
        => $"::User:: Id: {Id}, Name: {Name}, Email: {Email}, Slug: {Slug}";
  }
}