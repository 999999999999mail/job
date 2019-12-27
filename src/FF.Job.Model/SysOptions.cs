using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model
{
    public class SysOptions : IOptions<SysOptions>
    {
        public string Name { get; set; }
        public SysOptions Value => this;
    }
}
