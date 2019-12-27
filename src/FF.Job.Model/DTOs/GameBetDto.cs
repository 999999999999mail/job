using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FF.Job.Model.DTOs
{
    public class GameBetDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 投注结果
        /// </summary>
        public int BetResultType { get; set; }

        /// <summary>
        /// 投注类型，（自己平台游戏分类）
        /// </summary>
        public int BetType { get; set; }

        /// <summary>
        /// 游戏方游戏类型
        /// </summary>
        public string GameBetType { get; set; } = string.Empty;

        /// <summary>
        /// 游戏平台代码Code
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal BetAmount { get; set; }

        /// <summary>
        /// 有效投注
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 派彩
        /// </summary>
        public decimal Payout { get; set; }

        /// <summary>
        /// 投注数量
        /// </summary>
        public int BetNumber { get; set; }

        /// <summary>
        /// 用户游戏帐号【前缀+用户名】
        /// </summary>
        public string UserGameAccount { get; set; }

        /// <summary>
        /// 用户名【须小写】.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// 货币单位
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 游戏名
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// 游戏ID
        /// </summary>
        public string GameId { get; set; }

        /// <summary>
        /// 投注详情
        /// </summary>
        public string BetDetails { get; set; }

        /// <summary>
        /// 结算详情
        /// </summary>
        public string ResultGameDetails { get; set; }

        /// <summary>
        /// 注单号【数据中心唯一】，多平台游戏注单号存储一起，可能重复，需要自己处理，
        /// 需保证唯一，格式可自定义。
        /// 建议格式为：平台_游戏代码_游戏方注单号_自定义内容
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 是否推送
        /// </summary>
        public bool? IsPush { get; set; }

        /// <summary>
        /// 投注开始时间UTC0
        /// </summary>
        public DateTime? BetStartTime_GMT0 { get; set; }

        /// <summary>
        /// 投注结束时间UTC0
        /// </summary>
        public DateTime? BetEndTime_GMT0 { get; set; }

        /// <summary>
        /// 派彩时间UTC0 or 更新时间
        /// </summary>
        public DateTime? PayoutTime_GMT0 { get; set; }

        /// <summary>
        /// 代理号
        /// </summary>
        public string AgentCode { get; set; }

        /// <summary>
        /// Jackport奖金
        /// </summary>
        public decimal Jackpot { get; set; }


        /// <summary>
        /// jackpot投注金额
        /// </summary>
        public decimal JackPotBet { get; set; }

        /// <summary>
        /// 下载时间
        /// </summary>
        public DateTime? DownloadTime { get; set; }

        /// <summary>
        /// 业主代码
        /// </summary>
        public string OwnerCode { get; set; }

        /// <summary>
        /// 重新派彩的原始单号（本数据库内原始的OrderNumber ）
        /// </summary>          
        public string OriginalOrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// 游戏信息，游戏具体信息字段，json格式存放
        /// 通过字段BetType 投注类型，可以判断不同的存放数据格式，现在主要是体育、旗牌游戏信息会差别较大。
        /// 使用BetGameInfoDto与其子类序列化后存入此字段。
        /// </summary>
        public string GameInfo { get; set; }

        /// <summary>
        /// 是否重新派彩数据，默认为false，不是重新派彩
        /// </summary>
        public bool IsRePay { get; set; }

        /// <summary>
        /// 游戏局号
        /// </summary>
        public string Stage { get; set; }

        /// <summary>
        /// ff游戏数据版本号
        /// </summary>
        public long Version { get; set; }

        /// <summary>
        /// 游戏方的数据版本号
        /// </summary>
        public long GameThirdVersion { get; set; }

        /// <summary>
        /// 游戏方单号
        /// </summary>
        public string GameOrderNumber { get; set; } = string.Empty;
    }

    public enum GametBetType
    {
        UNKNOWN = 0,

        /// <summary>
        /// 1真人投注
        /// </summary>
        [Display(Name = "真人")]
        Trueman = 1,

        /// <summary>
        /// 2彩票投注
        /// </summary>
        [Display(Name = "彩票")]
        Lottery = 2,

        /// <summary>
        /// 3体育投注
        /// </summary>
        [Display(Name = "体育")]
        Sport = 3,

        /// <summary>
        /// 4电子投注
        /// </summary>
        [Display(Name = "电子")]
        Electron = 4,

        /// <summary>
        /// 5棋牌投注
        /// </summary>
        [Display(Name = "棋牌")]
        Chess = 5,

        /// <summary>
        /// 6电竞投注
        /// </summary>
        [Display(Name = "电竞")]
        Esports = 6,

        /// <summary>
        /// 7捕鱼投注
        /// </summary>
        [Display(Name = "捕鱼")]
        Fishing = 7,
    }

    public enum BetResultType
    {
        /// <summary>
        /// 1输
        /// </summary>
        [Display(Name = "输")]
        Lose = 1,

        /// <summary>
        /// 2赢
        /// </summary>
        [Display(Name = "赢")]
        Win = 2,

        /// <summary>
        /// 3和
        /// </summary>
        [Display(Name = "和")]
        Draw = 3,

        /// <summary>
        /// 4无效
        /// </summary>
        [Display(Name = "无效")]
        Invalid = 4,

        /// <summary>
        /// 冲正
        /// </summary>
        [Display(Name = "冲正")]
        Modified = 6
    }
}
