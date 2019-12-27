using FF.Job.Model;
using FF.Job.Model.DTOs;
using FF.Job.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FF.Job.Service.Impl
{
    public class GameService
    {
        private readonly IDbSessionFactory _dbSessionFactory;

        public GameService(IDbSessionFactory dbSessionFactory)
        {
            _dbSessionFactory = dbSessionFactory;
        }

        public async Task<GameDto> GetGameInfo(string ownerName, string gamePlatform)
        {
            using var session = _dbSessionFactory.OpenSession(ownerName);
            return await session.GetRepository<IGameRepository>().GetGameInfo(gamePlatform);
        }

        public async Task<IEnumerable<GameBetDto>> GetBetList(GameDto gameInfo)
        {
            return new List<GameBetDto>();
        }

        public async Task<IEnumerable<GameBetDto>> RemoveDuplicates(string ownerName, IEnumerable<GameBetDto> list)
        {
            if (list?.Count() > 0)
            {
                var versionKey = list.Min(p => p.Version);
                using var session = _dbSessionFactory.OpenSession(ownerName);
                var temp = await session.GetRepository<IGameRepository>().GetExistingData(list.First().Platform, versionKey);
                if (temp?.Count() > 0)
                {
                    return list.Where(p => !temp.Contains(p.OrderNumber));
                }
            }
            return list;
        }

        public async Task<IEnumerable<BetDto>> ConvertData(string ownerName, IEnumerable<GameBetDto> list, GameDto gameInfo)
        {
            var result = new List<BetDto>();

            var userNames = list.Select(p => p.UserName).Distinct();
            IDictionary<string, int> userIds;
            using (var session = _dbSessionFactory.OpenSession(ownerName))
            {
                userIds = await session.GetRepository<IGameRepository>().GetUserIds(userNames);
            }

            foreach (var item in list)
            {
                if (userIds.ContainsKey(item.UserName))
                {
                    result.Add(new BetDto
                    {
                        ApiId = gameInfo.ApiId,
                        GamePlatform = item.Platform,
                        UserId = userIds[item.UserName],
                        UserName = item.UserName,
                        Num = item.BetNumber,
                        Bet = item.BetAmount,
                        RealBet = item.RealAmount,
                        PayOut = item.Payout,
                        Type = item.BetType,
                        CreateTime = item.BetStartTime_GMT0.Value,
                        UpdateTime = item.BetStartTime_GMT0.Value,
                        GameNameId = item.GameId,
                        TableId = string.Empty,
                        ResultType = item.BetResultType,
                        Stage = item.Stage,
                        BetBeginTime = item.BetStartTime_GMT0.Value,
                        BetEndTime = item.BetStartTime_GMT0.Value,
                        OrderNumber = item.GameOrderNumber,
                        Jackpot = item.Jackpot,
                        BetDetail = item.BetDetails,
                        ResultDetail = item.ResultGameDetails
                    });
                }
            }

            return result;
        }

        public IEnumerable<BetTempStatDto> Stat(IEnumerable<BetDto> bets)
        {
            var betTempStats = bets
                .GroupBy(p => new { p.UpdateTime, p.UserId, p.ApiId, p.Type })
                .Select(p => new BetTempStatDto
                {
                    BetDate = Convert.ToDateTime(p.FirstOrDefault().UpdateTime),
                    UserId = p.FirstOrDefault().UserId,
                    ApiId = p.FirstOrDefault().ApiId,
                    Type = p.FirstOrDefault().Type,
                    Num = p.Sum(c => c.Num),
                    Bet = p.Sum(c => c.Bet),
                    RealBet = p.Sum(c => c.RealBet),
                    PayOut = p.Sum(c => c.PayOut),
                    Jackpot = p.Sum(c => c.Jackpot),
                    Jackpot_Grand = p.Sum(c => c.GameNameId == "Grand" ? c.Jackpot : 0),
                    Jackpot_Major = p.Sum(c => c.GameNameId == "Major" ? c.Jackpot : 0),
                    Jackpot_Minor = p.Sum(c => c.GameNameId == "Minor" ? c.Jackpot : 0),
                    Jackpot_Mini = p.Sum(c => c.GameNameId == "Mini" ? c.Jackpot : 0),
                    CreateTime = DateTime.UtcNow
                });
            return betTempStats;
        }

        public async Task<bool> Save()
        {
            return false;
        }
    }
}
