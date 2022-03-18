namespace Blog.Models;
public class PostWithTagCount
{
    public String? Title { get; set; }
    public int TagCount { get; set; }

    public override string ToString()
    {
        return $" Title: {Title}, Tag Count: {TagCount}";
    }
}