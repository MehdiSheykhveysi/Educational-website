using System.Collections.Generic;

namespace Site.Web.Models.PagesModels.CourseEpisodManageModel
{
    public class EpisodIndextVm : EpisodBaseVm
    {
        public EpisodIndextVm()
        {
            this.Episods = new List<EpisodFullBaseVm>();
        }

        public List<EpisodFullBaseVm> Episods { get; set; }
    }
}
