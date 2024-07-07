using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.ViewComponents;

public class RecentPostsMenu : ViewComponent
{
    IPostRepository _postRepo;

    public RecentPostsMenu(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public IViewComponentResult Invoke()
    {
        return View(_postRepo
                            .Posts
                            .OrderByDescending(p => p.PublishedOn)
                            .Take(5)
                            .ToList()
                    );
    }
}