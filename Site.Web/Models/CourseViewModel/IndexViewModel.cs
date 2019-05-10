namespace Site.Web.Models.CourseViewModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Paging= new PagingViewModel();
        }
        public PagingViewModel Paging { get; set; }

        public int PageNumber { get; set; } = 1;
        public string Searchkeyvalue { get; set; }
        public string KeyWordTitle { get; set; }
    }
}
