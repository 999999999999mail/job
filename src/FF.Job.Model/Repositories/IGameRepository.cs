using FF.Job.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FF.Job.Model.Repositories
{
    public interface IGameRepository: IRepository
    {
        Task<GameDto> GetGameInfo(string gamePlatform);

        Task<IEnumerable<string>> GetExistingData(string gamePlatform, long versionKey);

        Task<IDictionary<string, int>> GetUserIds(IEnumerable<string> userNames);
    }
}
