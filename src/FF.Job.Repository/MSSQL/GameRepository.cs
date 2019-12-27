using Dapper;
using FF.Job.Model.DTOs;
using FF.Job.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Job.Repository.MSSQL
{
    public class GameRepository : RepositoryBase, IGameRepository
    {
        public async Task<GameDto> GetGameInfo(string gamePlatform)
        {
            var sql = "SELECT TOP 1 ApiId, GamePlatform, DownloadLastVersionKeyAuto, DownloadLastDataTimeAuto FROM [u8_GameDataSyncTask] WHERE GamePlatform = @GamePlatform";
            return await DbSession.Connection.QueryFirstOrDefaultAsync<GameDto>(sql, new { gamePlatform });
        }

        public async Task<IEnumerable<string>> GetExistingData(string gamePlatform, long versionKey)
        {
            var sql = $"SELECT OrderNumber FROM [u8_GameSyncDataCheck] WHERE GamePlatform = @GamePlatform AND [Version] > {versionKey}";
            var list = await DbSession.Connection.QueryAsync<string>(sql, new { gamePlatform });
            return list;
        }

        public async Task<IDictionary<string, int>> GetUserIds(IEnumerable<string> userNames)
        {
            var dic = new Dictionary<string, int>();
            var sql = $"SELECT Id, UserName FROM [u8_User] WHERE UserName IN @UserNames";
            using (var reader = await DbSession.Connection.ExecuteReaderAsync(sql, new { userNames }))
            {
                while (reader.Read())
                {
                    dic.TryAdd(reader.GetString(0), reader.GetInt32(1));
                }
            }
            return dic;
        }
    }
}
