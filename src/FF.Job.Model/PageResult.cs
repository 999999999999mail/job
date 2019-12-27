using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model
{
    public class PageResult<T> : ResultBase
    {
        public IEnumerable<T> Rows { get; set; }

        public int RecordCount { get; set; }
    }
}
