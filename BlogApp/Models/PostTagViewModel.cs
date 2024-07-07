using BlogApp.Entity;

namespace BlogApp.Models;

public class PostTagViewModel
{
    public List<Post> Posts { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
}