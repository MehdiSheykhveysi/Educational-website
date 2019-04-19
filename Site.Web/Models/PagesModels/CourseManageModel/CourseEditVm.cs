namespace Site.Web.Models.PagesModels.CourseManageModel
{
    public class CourseEditVm : CourseFullBaseModel
    {
        public CourseEditVm() : base()
        {
            
        }
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
    }
}
