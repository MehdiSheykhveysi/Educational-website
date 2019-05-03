using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Site.Core.Infrastructures.DTO;
using Site.Web.Infrastructures;
using Site.Web.Models.HomeViewModel;

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
        public string PageSearchkeyValue { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
        public bool PageIsdeleted { get; set; }
        public SearchParameterVm PageSearchParameter { get; set; }

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

                    if (PageSearchParameter != null)
                    {
                        string queryStrings = PageSearchParameter.CourseGroups.ToQueryString();

                        a.Attributes["href"] = urlHelper.Action(PageAction, PageController, new
                        {
                            Model_PageNumber = i,
                            Model_Searckkeyvalue = PageSearchkeyValue,
                            Model_SearchParameter_PriceStatusType = PageSearchParameter.PriceStatusType,
                            Model_SearchParameter_OrderStatusType = PageSearchParameter.OrderStatusType,
                            Model_SearchParameter_StartingPrice = PageSearchParameter.StartingPrice,
                            Model_SearchParameter_EndOfPrice = PageSearchParameter.EndOfPrice,
                            Model_SearchParameter_CourseGroups = queryStrings
                        });
                    }
                    else
                        a.Attributes["href"] = urlHelper.Action(PageAction, PageController, new
                        {
                            PageNumber = i,
                            Searckkeyvalue = PageSearchkeyValue,
                            IsDeleted = PageIsdeleted
                        });
                }
                else
                {
                    a.Attributes["class"] = (i == PageData.CurentItem) ? "paginate_button active" : "paginate_button";
                    a.Attributes["href"] = urlHelper.Page(PageName, new { PageNumber = i, Searckkeyvalue = PageSearchkeyValue, IsDeleted = PageIsdeleted });
                }
                a.InnerHtml.Append(i.ToString());
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(ul);
        }

    }
}