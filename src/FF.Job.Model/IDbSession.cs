using FF.Job.Infrastructure;
using FF.Job.Model.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FF.Job.Model
{
    public interface IDbSession : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();

        T GetRepository<T>() where T : class, IRepository;
    }

    public class SqlSession : IDbSession
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IDictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();

        internal SqlSession(IServiceProvider serviceProvider, string connectionString)
        {
            _serviceProvider = serviceProvider;
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }

        private IDbConnection _connection = null;

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    if (string.IsNullOrEmpty(ConnectionString))
                    {
                        throw new CustomException("The ConnectionString property has not been initialized.");
                    }
                    _connection = new SqlConnection(ConnectionString);
                }
                return _connection;
            }
        }

        public IDbTransaction Transaction { get; private set; } = null;

        public void BeginTransaction()
        {
            if (Transaction == null)
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                Transaction = Connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            try
            {
                Transaction?.Commit();
            }
            catch
            {
                Transaction?.Rollback();
                throw;
            }
            Transaction?.Dispose();
            Transaction = null;
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            Transaction?.Dispose();
            Transaction = null;
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            var type = typeof(T);
            if (_repositories.Keys.Contains(type))
            {
                return _repositories[type] as T;
            }

            T repo = _serviceProvider.GetRequiredService<T>();
            repo.DbSession = this;
            _repositories.Add(type, repo);
            return repo;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                if (Transaction != null)
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
