using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using Site.Core.Infrastructures.Utilities.Enums;
using Site.Core.Infrastructures.Utilities.Extensions;
using System.Collections.Generic;
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

        public async Task<PagedResult<Course>> GetPagedCourseAsync(string Title, bool IsDeleted, int Count, int CurrentNumber, CancellationToken cancellationToken,
            PriceStatusType PrisceStatusType = PriceStatusType.All, OrderStatusType orderStatusType = OrderStatusType.Default, int StartingPrice = 0, int EndOfPrice = 0,
            IEnumerable<int> SelectedGroup = null, string KeyWordTitle = "")
        {
            PagedResult<Course> paged = new PagedResult<Course>();

            int ListCount = await NoTrackEntities.SmartWhere(Title, IsDeleted, SelectedGroup, StartingPrice, EndOfPrice, PrisceStatusType, KeyWordTitle).CountAsync(cancellationToken);

            IQueryable<Course> query = NoTrackEntities.SmartWhere(Title, IsDeleted, SelectedGroup, StartingPrice, EndOfPrice, PrisceStatusType, KeyWordTitle).SmartOrderByStatus(orderStatusType);

            paged.ListItem = await query.Skip((CurrentNumber - 1) * Count).Take(Count)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            paged.PageData.CurentItem = CurrentNumber;
            paged.PageData.TotalItem = ListCount;
            paged.PageData.ItemPerPage = Count;
            return paged;
        }

        public Task<Course> GetCourseWithKeyWordsAsync(int Id, CancellationToken cancellationToken)
        {
            return Entities.Where(c => c.Id == Id).Include(c => c.Keywordkeys).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CourseDetailDTO> GetCourseDetailAsync(int CourseId, CancellationToken cancellationToken)
        {
            CourseDetailDTO course = await NoTrackEntities.Include(c => c.CourseEpisods).Include(c => c.CourseStatus)
                .Include(c => c.CourseLevel).Include(c => c.CustomUser).Include(c => c.Keywordkeys)
                .Select(c => new CourseDetailDTO
                {
                    CourseID = c.Id,
                    CourseDescription = c.CourseDescription,
                    CourseLevelTitle = c.CourseLevel.Title,
                    CoursePrice = c.CoursePrice,
                    CourseStatusTitle = c.CourseStatus.Title,
                    CourseTitle = c.CourseTitle,
                    CreateDate = c.CreateDate,
                    DemoFileName = c.DemoFileName,
                    Episods = c.CourseEpisods.Select(e => new EpisodDTO { EpisodeTime = e.EpisodeTime, EpisodID = e.Id, EpisodTitle = e.Title, IsFree = e.IsFree }).ToList(),
                    Keywords = c.Keywordkeys.Select(k => new KeywordDTO { Id = k.Id, Title = k.Title }).ToList(),
                    ImageName = c.ImageName,
                    TeacherAvatar = c.CustomUser.Avatar,
                    TeacherID = c.CustomUser.Id,
                    TeacherUserName = c.CustomUser.ShowUserName,
                    TotalEpisodTime = c.TotalEpisodTime,
                    UpdateDate = c.UpdateDate

                }).FirstOrDefaultAsync(c => c.CourseID == CourseId, cancellationToken);
            return course;
        }
        
    }
}