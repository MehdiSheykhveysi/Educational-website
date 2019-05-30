using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class CourseGroupRepository : GenericRepositories<CourseGroup>, ICourseGroupRepository
    {
        public CourseGroupRepository(LearningSiteDbContext context) : base(context)
        {
        }

        public async Task<List<CourseGroupDTO>> GetGroupWithSubGroupAsync(CancellationToken cancellationToken, string GroupName, bool IsDeleted = false)
        {
            List<CourseGroupDTO> Entites = await NoTrackEntities.Where(cg => (string.IsNullOrEmpty(GroupName) || EF.Functions.Like(cg.Title, $"%{GroupName}%")) && cg.IsDeleted == IsDeleted)
                .Select(g => new CourseGroupDTO
                {
                    Groups = g.Groups.Where(sg => sg.IsDeleted == IsDeleted && sg.ParentId == g.Id)
                    .Select(sg => new SubCourseGropDTO
                    {
                        GroupTitle = sg.Title,
                        Id = sg.Id
                    }).ToList(),
                    ParentId = g.ParentId,
                    ID = g.Id,
                    ParentTitle = g.Title
                })
                .ToListAsync(cancellationToken);

            return Entites;
        }
    }
}
