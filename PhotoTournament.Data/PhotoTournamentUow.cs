using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Model;

namespace PhotoTournament.Data
{
    public class PhotoTournamentUow : IPhotoTournamentUow, IDisposable
    {
        private PhotoTournamentDbContext DbContext { get; set; }

        //Constructor for testing purposes
        public PhotoTournamentUow()
        {
            CreateDbContext();
            RepositoryProvider = new RepositoryProvider(new RepositoryFactories());
        }
        public PhotoTournamentUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();
            repositoryProvider.DbContext = DbContext;
            RepositoryProvider = repositoryProvider;
        }

        protected void CreateDbContext()
        {
            DbContext = new PhotoTournamentDbContext();
            DbContext.Configuration.ProxyCreationEnabled = false;
            DbContext.Configuration.LazyLoadingEnabled = false;
            DbContext.Configuration.ValidateOnSaveEnabled = false;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public virtual IWinnerRepository Winners { get { return GetRepo<IWinnerRepository>(); } }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
    }
}
