using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class CourseRepository : GenericRepositories<Course>, ICourseRepository
    {
        public CourseRepository(LearningSiteDbContext Context) : base(Context)
        {

        }
        public async Task<PagedResult<Course>> GetPagedCourseAsync(string Title, bool IsDeleted, int Count, int CurrentNumber, CancellationToken cancellationToken)
        {
            PagedResult<Course> paged = new PagedResult<Course>();

            int ListCount = await NoTrackEntities.CountAsync(cancellationToken);

            paged.ListItem = await NoTrackEntities.Where(u => (string.IsNullOrEmpty(Title) || u.CourseTitle.IndexOf(Title, StringComparison.CurrentCultureIgnoreCase) != -1) && u.IsDeleted == IsDeleted).OrderBy(u => u.CourseTitle).Skip((CurrentNumber - 1) * Count).Take(Count).AsNoTracking().ToListAsync(cancellationToken);
            paged.PageData.CurentItem = CurrentNumber;
            paged.PageData.TotalItem = ListCount;
            paged.PageData.ItemPerPage = Count;
            return paged;
        }
        public Task<Course> GetCourseWithKeyWordsAsync(int Id,CancellationToken cancellationToken)
        {
            return Entities.Where(c => c.Id == Id).Include(c => c.Keywordkeys).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
