using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Site.Core.Infrastructures.DTO;

namespace Site.Core.DataBase.Repositories
{
    public interface ISpReaderRepository
    {
        Task<List<SpDTO>> GetSpsAsync(string[,] Parameters, CancellationToken cancellationToken);
    }
}