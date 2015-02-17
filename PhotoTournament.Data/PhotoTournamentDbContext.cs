using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Model;

namespace PhotoTournament.Data
{
    public class PhotoTournamentDbContext : DbContext
    {
        public PhotoTournamentDbContext() : base("name=PhotoTournamentDbContext"){}

        public virtual DbSet<Winner> Winners { get; set; }

    }
}
