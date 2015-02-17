using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Model;

namespace PhotoTournament.Data
{
    public interface IWinnerRepository : IRepository<Winner>
    {
        ICollection<Winner> GetLastTenWinners();
        Winner GetLatestWinnerByUsername(string username);

    }
}
