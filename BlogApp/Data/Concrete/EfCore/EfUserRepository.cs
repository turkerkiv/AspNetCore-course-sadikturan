using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfUserRepository : IUserRepository
{
    public IQueryable<User> Users => _context.Users;
    private BlogContext _context;

    public EfUserRepository(BlogContext context)
    {
        _context = context;
    }

    public void CreateUser(User User)
    {
        _context.Users.Add(User);
        _context.SaveChanges();
    }
}