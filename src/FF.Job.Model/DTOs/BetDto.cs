using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model.DTOs
{
    public class BetDto
    {
        public long Id { get; set; }

        public string GamePlatform { get; set; }

        /// <summary>
        /// 游戏类别
        /// </summary>
        public string GameType { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        /// <summary>
        /// 投注次数
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 总投注额
        /// </summary>
        public decimal Bet { get; set; }

        /// <summary>
        /// 有效投注额
        /// </summary>
        public decimal RealBet { get; set; }

        /// <summary>
        /// 派彩
        /// </summary>
        public decimal PayOut { get; set; }

        /// <summary>
        /// 投注类型 1：真人投注额 2：彩票投注额 3：体育投注额 4：电子投注额
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 投注时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 局号
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// 桌号
        /// </summary>
        public string TableId { get; set; }

        /// <summary>
        /// 游戏类别
        /// </summary>
        public string GameNameId { get; set; }

        /// <summary>
        /// 游戏结果
        /// </summary>
        public int ResultType { get; set; }

        /// <summary>
        /// 注单号（游戏投注业务主键ID）
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 分组后投注最小时间,GMT-5
        /// </summary>
        public DateTime BetBeginTime { get; set; }

        /// <summary>
        /// 分组后投注最大时间,GMT-5
        /// </summary>
        public DateTime BetEndTime { get; set; }

        /// <summary>
        /// 更新时间，一般为投注时间，等于CreateTimes，只有体育投注，结算后会更新该字段，解决优惠发放问题。
        /// 在界面上一般使用CreateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 通过补单新增
        /// </summary>
        public bool IsRevised { get; set; }

        /// <summary>
        /// 彩金
        /// </summary>
        public decimal Jackpot { get; set; }

        public int ApiId { get; set; }

        /// <summary>
        /// 下注明细
        /// </summary>
        public string BetDetail { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        public string ResultDetail { get; set; }
    }
}
