using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfCommentRepository : ICommentRepository
{
    public IQueryable<Comment> Comments => _context.Comments;
    private BlogContext _context;

    public EfCommentRepository(BlogContext context)
    {
        _context = context;
    }

    public void CreateComment(Comment Comment)
    {
        _context.Comments.Add(Comment);
        _context.SaveChanges();
    }
}