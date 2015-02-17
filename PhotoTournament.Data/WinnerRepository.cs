using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Model;

namespace PhotoTournament.Data
{
    public class WinnerRepository : EFRepository<Winner>, IWinnerRepository
    {
        public ICollection<Winner> GetLastTenWinners()
        {
            return DbSet.Select(s => s).ToList();
        }

        public virtual Winner GetLatestWinnerByUsername(string username)
        {
            var winner = DbSet.Where(s => s.Username == username)
                               .OrderByDescending(s => s.DateCreated)
                               .FirstOrDefault();
            return winner;
        }

        public WinnerRepository(DbContext dbContext) : base(dbContext) {}
    }
}
