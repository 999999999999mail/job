using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Infrastructure
{
    public class CustomException : Exception
    {
        public int Code { get; private set; }

        public CustomException(string message, int code = 0)
            : base(message)
        {
            Code = code;
        }
    }
}
