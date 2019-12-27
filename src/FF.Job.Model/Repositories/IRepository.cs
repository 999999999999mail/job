using System;
using System.Collections.Generic;
using System.Text;

namespace FF.Job.Model.Repositories
{
    public interface IRepository
    {
        IDbSession DbSession { get; set; }
    }
}
