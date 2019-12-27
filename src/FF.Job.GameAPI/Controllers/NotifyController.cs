using FF.Job.Model;
using FF.Job.Model.DTOs;
using FF.Job.Service;
using Microsoft.AspNetCore.Mvc;

namespace FF.Job.GameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly PullGameDataWorker _pullGameDataJob;
        public NotifyController(PullGameDataWorker pullGameDataJob)
        {
            _pullGameDataJob = pullGameDataJob;
        }

        [HttpPost("PullGameData")]
        public DataResult<bool> PullGameData([FromBody]PullGameDataDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.GamePlatform))
            {
                return ResultFactory.False("请求参数错误");
            }
            _pullGameDataJob.Start(dto.GamePlatform);
            return ResultFactory.True();
        }
    }
}