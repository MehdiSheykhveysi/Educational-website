using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Site.Core.Infrastructures.DTO;

namespace Site.Web.TagHelpers
{
    [HtmlTargetElement("nav", Attributes = "Page-data")]
    public class PagerTagHelper : TagHelper
    {
        public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        public IUrlHelperFactory _urlHelperFactory { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageData PageData { get; set; }
        public string PageName { get; set; }
        public string PageUsername { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes["class"] = "pagination";
            for (int i = 1; i <= PageData.TotalPages(); i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                if (string.IsNullOrEmpty(PageName) || !string.IsNullOrEmpty(PageAction))
                {
                    a.Attributes["class"] = (i == PageData.CurentItem) ? "paginate_button active" : "paginate_button";
                    a.Attributes["href"] = urlHelper.Action(PageAction, PageController, new { PageNumber = i, UserName = PageUsername });
                }
                else
                {
                    a.Attributes["class"] = (i == PageData.CurentItem) ? "paginate_button active" : "paginate_button";
                    a.Attributes["href"] = urlHelper.Page(PageName, new { PageNumber = i, UserName = PageUsername });
                }
                a.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(ul);
        }

    }
}