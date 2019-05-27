using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface ICommentRepository : IGenericRepositories<Comment>
    {
        Task<PagedResult<CommentDTO>> GetPagedComment(int CourseId, int CurrentPageNumber, int TakeCount, CancellationToken cancellationToken);
    }
}
