using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoTournament.Data
{
    public class RepositoryFactories
    {
        private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;

        public RepositoryFactories()
        {
            _repositoryFactories = new Dictionary<Type, Func<DbContext, object>>()
            {
                {typeof(IWinnerRepository), dbContext => new WinnerRepository(dbContext)}
            };
        }

        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EFRepository<T>(dbContext);
        }
    }
}
