using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class CommentRepository : GenericRepositories<Comment>, ICommentRepository
    {
        public CommentRepository(LearningSiteDbContext context) : base(context)
        {

        }

        public async Task<PagedResult<CommentDTO>> GetPagedComment(int CourseId, int CurrentPageNumber, int TakeCount, CancellationToken cancellationToken)
        {
            PagedResult<CommentDTO> result = new PagedResult<CommentDTO>();
            int totalCount = await NoTrackEntities.CountAsync(c => c.CoourseId == CourseId && !c.IsDeleted, cancellationToken);

            result.ListItem = await NoTrackEntities.Where(c => c.CoourseId == CourseId && !c.IsDeleted)
                .OrderByDescending(c => c.CreateTime)
                .Skip((CurrentPageNumber - 1) * TakeCount)
                .Select(c => new CommentDTO
                {
                    Body = c.Body,
                    Name = c.Name,
                    UserAvatar = c.User.Avatar,
                    CreateTime = c.CreateTime
                }).Take(TakeCount)
            .ToListAsync(cancellationToken);

            result.PageData.CurentItem = CurrentPageNumber;
            result.PageData.ItemPerPage = TakeCount;
            result.PageData.TotalItem = totalCount;

            return result;
        }
    }
}
