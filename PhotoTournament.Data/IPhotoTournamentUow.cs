using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Model;

namespace PhotoTournament.Data
{
    public interface IPhotoTournamentUow
    {
        void Commit();

        IWinnerRepository Winners { get; }
    }
}
