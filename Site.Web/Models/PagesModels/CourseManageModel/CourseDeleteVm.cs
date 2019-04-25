namespace Site.Web.Models.PagesModels.CourseManageModel
{
    public class CourseDeleteVm : CourseFullBaseModel
    {
        public CourseDeleteVm() : base()
        {

        }
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
    }
}