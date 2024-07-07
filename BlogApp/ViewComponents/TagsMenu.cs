using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.ViewComponents;

public class TagsMenu : ViewComponent
{
    ITagRepository _tagRepo;
    public TagsMenu(ITagRepository tagRepository)
    {
        _tagRepo = tagRepository;
    }

    public IViewComponentResult Invoke()
    {
        return View(_tagRepo.Tags.ToList());
    }
}