using FF.Job.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.Job.Model
{
    public interface IDbSessionFactory
    {
        IDbSession OpenSession(string database = null);
    }

    public class DbSessionFactory : IDbSessionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly DbOptions _options;

        public DbSessionFactory(IServiceProvider serviceProvider, IOptions<DbOptions> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public IDbSession OpenSession(string database = null)
        {
            string connectionString;
            if (string.IsNullOrEmpty(database))
            {
                connectionString = _options?.ConnectionStrings?.FirstOrDefault()?.Value;
            }
            else
            {
                connectionString = _options?.ConnectionStrings?.FirstOrDefault(p => p.Name == database)?.Value;
            }
            return new SqlSession(_serviceProvider, connectionString);
        }
    }

    public static class DbSessionFactoryServiceCollectionExtensions
    {
        public static IServiceCollection AddDbSessionFactory(this IServiceCollection services, IConfigurationSection configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (!configuration.Exists())
            {
                throw new ArgumentNullException($"{configuration.Key} in appsettings.json");
            }
            services.Configure<DbOptions>(configuration);
            services.AddScoped<IDbSessionFactory, DbSessionFactory>();
            return services;
        }
    }
}
