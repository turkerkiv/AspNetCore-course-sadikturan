using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfTagRepository : ITagRepository
{
    public IQueryable<Tag> Tags => _context.Tags;
    private BlogContext _context;

    public EfTagRepository(BlogContext context)
    {
        _context = context;
    }

    public void CreateTag(Tag tag)
    {
        _context.Tags.Add(tag);
        _context.SaveChanges();
    }
}