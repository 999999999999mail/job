using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FF.Job.Model
{
    public interface IAggregateRoot
    {
    }

    public class EntityRoot : IAggregateRoot
    {
        [JsonIgnore]
        public int __RecordCount { get; set; }
    }
}
