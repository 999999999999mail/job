using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FF.Job.Model
{
    public class ResultBase
    {
        public ResultBase()
        {
        }

        public ResultBase(double code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// 消息代码 
        /// 1: 成功，其他为失败
        /// </summary>
        public double Code { get; set; }

        [JsonIgnore]
        public bool Success
        {
            get
            {
                return Code == 1;
            }
        }

        public string Message { get; set; }
    }
}
