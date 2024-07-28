using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.TagHelpers;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper : TagHelper
{
    [ViewContext]
    public ViewContext? ViewContext { get; set; }
    [HtmlAttributeName("page-model")]
    public PageInfo? PageModel { get; set; }
    [HtmlAttributeName("page-action")]
    public string? PageAction { get; set; }
    public string PageClass { get; set; } = string.Empty;
    public string PageClassLink { get; set; } = string.Empty;
    public string PageClassActive { get; set; } = string.Empty;

    IUrlHelperFactory _urlHelperFac;

    public PageLinkTagHelper(IUrlHelperFactory urlhelp)
    {
        _urlHelperFac = urlhelp;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext != null && PageModel != null)
        {
            var urlHelper = _urlHelperFac.GetUrlHelper(ViewContext);
            TagBuilder div = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder link = new TagBuilder("a");
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });

                link.AddCssClass(PageClass);
                link.AddCssClass(i == PageModel.CurrentPage ? PageClassActive : PageClassLink);
                link.InnerHtml.Append(i.ToString());
                div.InnerHtml.AppendHtml(link);
            }

            output.Content.AppendHtml(div.InnerHtml);
        }
    }
}