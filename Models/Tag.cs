
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public String? Slug { get; set; }
        public IList<Post>? Posts { get; set; }

        public override String ToString()
        {
            return $"TAG{{ Id: {Id}, Name: {Name}, Slug: {Slug} }}";
        }
    }
}