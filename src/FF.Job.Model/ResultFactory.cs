using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model
{
    public static class ResultFactory
    {
        public static DataResult<T> Ok<T>(T data, string message = "操作成功")
        {
            return new DataResult<T>
            {
                Data = data,
                Message = message,
                Code = 1
            };
        }

        public static DataResult<T> Bad<T>(string message = "操作失败", double code = 0, T data = default)
        {
            return new DataResult<T>()
            {
                Data = data,
                Message = message,
                Code = code == 1 ? 0 : code
            };
        }

        public static DataResult<bool> True(string message = "操作成功")
        {
            return Ok(true, message);
        }

        public static DataResult<bool> False(string message = "操作失败", double code = 0)
        {
            return Bad(message, code, false);
        }

        public static PageResult<T> Page<T>(IEnumerable<T> rows, int recordCount, string message = "操作成功")
        {
            return new PageResult<T>
            {
                Rows = rows,
                RecordCount = recordCount,
                Message = message,
                Code = 1
            };
        }
    }
}
