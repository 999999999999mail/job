using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model.DTOs
{
    public class BetTempStatDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime BetDate { get; set; }

        public int ApiId { get; set; }

        public int Type { get; set; }

        public int Num { get; set; }

        public decimal Bet { get; set; }

        public decimal RealBet { get; set; }

        public decimal PayOut { get; set; }

        public decimal Jackpot { get; set; }

        public decimal Jackpot_Grand { get; set; }

        public decimal Jackpot_Major { get; set; }

        public decimal Jackpot_Minor { get; set; }

        public decimal Jackpot_Mini { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
