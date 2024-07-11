using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    readonly IPostRepository _postRepo;
    readonly ICommentRepository _commentRepo;

    public PostsController(IPostRepository postRepo, ICommentRepository commentRepository)
    {
        _commentRepo = commentRepository;
        _postRepo = postRepo;
    }

    public async Task<IActionResult> Index(string tag)
    {
        var posts = _postRepo.Posts;
        var claims = User.Claims;

        if (!string.IsNullOrEmpty(tag))
        {
            posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
        }

        return View(
            new PostTagViewModel
            {
                Posts = await posts.Include(p => p.Tags).ToListAsync(),
            }
        );
    }

    public async Task<IActionResult> Details(string url)
    {
        return View(await _postRepo.Posts.Include(p => p.Tags).Include(p => p.Comments).ThenInclude(c => c.User).FirstOrDefaultAsync(p => p.Url == url));
    }

    [HttpPost]
    public JsonResult AddComment(int postId, string text)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var username = User.FindFirstValue(ClaimTypes.Name);
        var avatar = User.FindFirstValue(ClaimTypes.UserData);

        var entity = new Comment
        {
            Text = text,
            PublishedOn = DateTime.UtcNow,
            PostId = postId,
            UserId = int.Parse(userId ?? ""),
        };
        _commentRepo.CreateComment(entity);
        return Json(new
        {
            username,
            text,
            publishedOn = entity.PublishedOn.ToString("d"),
            image = avatar,
        });
    }
}