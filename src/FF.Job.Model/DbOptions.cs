using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model
{
    public class DbOptions : IOptions<DbOptions>
    {
        public IEnumerable<ConnectionString> ConnectionStrings { get; set; }

        public DbOptions Value => this;
    }

    public class ConnectionString
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
