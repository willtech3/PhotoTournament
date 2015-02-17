using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoTournament.Model
{
    public class Winner
    {
        public virtual int WinnerId { get; set; }
        public virtual string Username { get; set; }
        public virtual string CatPictureUrl { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
