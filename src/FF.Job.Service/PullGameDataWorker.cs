using FF.Job.Model;
using FF.Job.Service.Impl;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace FF.Job.Service
{
    public class PullGameDataWorker
    {
        private readonly ILogger _logger;

        private readonly DbOptions _dbOptions;

        private readonly ConcurrentDictionary<string, string> _dic = new ConcurrentDictionary<string, string>();

        private readonly GameService _gameService;

        public PullGameDataWorker(ILogger<PullGameDataWorker> logger, IOptions<DbOptions> options, GameService gameService)
        {
            _logger = logger;
            _dbOptions = options.Value;
            _gameService = gameService;
        }

        public void Start(string gamePlatform)
        {
            foreach (var item in _dbOptions.ConnectionStrings)
            {
                ExecAsync(gamePlatform, item.Name);
            }
        }

        public void ExecAsync(string gamePlatform, string ownerName)
        {
            var key = $"{gamePlatform}_{ownerName}";
            if (_dic.TryAdd(key, ownerName))
            {
                Task.Run(async () =>
                {
                    try
                    {
                        _logger.LogTrace($"{key} 开始获取游戏信息");
                        var gameInfo= await _gameService.GetGameInfo(ownerName, gamePlatform);
                        if (gameInfo == null)
                        {
                            return;
                        }

                        _logger.LogTrace($"{key} 开始下载投注数据");
                        var list = await _gameService.GetBetList(gameInfo);
                        if ((list?.Count() ?? 0) == 0)
                        {
                            return;
                        }

                        _logger.LogTrace($"{key} 开始删除重复数据");
                        var list2 = await _gameService.RemoveDuplicates(ownerName, list);
                        if ((list2?.Count() ?? 0) == 0)
                        {
                            return;
                        }

                        _logger.LogTrace($"{key} 开始转换投注数据");
                        var bets = await _gameService.ConvertData(ownerName, list2, gameInfo);
                        if ((bets?.Count() ?? 0) == 0)
                        {
                            return;
                        }

                        _logger.LogTrace($"{key} 开始统计投注数据");
                        var stats = _gameService.Stat(bets);

                        _logger.LogTrace($"{key} 开始保存数据");
                        var save = await _gameService.Save();

                        _ = Task.Run(async () =>
                          {
                              _logger.LogTrace($"{key} 开始发放优惠");
                              await Task.Delay(6000);
                          });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"{key} 任务执行异常");
                    }
                    finally
                    {
                        if (!_dic.TryRemove(key, out _))
                        {
                            _logger.LogError($"{key} 任务删除失败");
                        }
                    }
                });
            }
            else
            {
                _logger.LogWarning($"{key} 下载任务添加失败");
            }
        }
    }
}
