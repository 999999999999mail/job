using FF.Job.Infrastructure;
using FF.Job.Model;
using System;

namespace FF.Job.Repository
{
    public class RepositoryBase
    {
        protected RepositoryBase()
        {
        }

        private IDbSession _dbSession;
        public IDbSession DbSession
        {
            set { _dbSession = value; }
            get
            {
                if (_dbSession == null)
                {
                    throw new CustomException("The DbSession property has not been initialized.");
                }
                return _dbSession;
            }
        }
    }
}
