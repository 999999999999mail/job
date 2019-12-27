using System;

namespace FF.Job.Model
{
    public class DataResult<T> : ResultBase
    {
        public T Data { get; set; }
    }
}
