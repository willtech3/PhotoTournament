using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Model;

namespace PhotoTournament.Data
{
    public class PhotoTournamentDatabaseInitializer : DropCreateDatabaseIfModelChanges<PhotoTournamentDbContext>
    {
        protected override void Seed(PhotoTournamentDbContext context)
        {
            var winners = new List<Winner>
            {
                new Winner() {Username = "buddyroach1", CatPictureUrl = "http://pv.pop.umn.edu/images/079.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach2", CatPictureUrl = "http://pv.pop.umn.edu/images/042.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach4", CatPictureUrl = "http://pv.pop.umn.edu/images/0075.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach5", CatPictureUrl = "http://pv.pop.umn.edu/images/070.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach6", CatPictureUrl = "http://pv.pop.umn.edu/images/100.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach7", CatPictureUrl = "http://pv.pop.umn.edu/images/109.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach8", CatPictureUrl = "http://pv.pop.umn.edu/images/094.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach9", CatPictureUrl = "http://pv.pop.umn.edu/images/025.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach10", CatPictureUrl = "http://pv.pop.umn.edu/images/047.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach11", CatPictureUrl = "http://pv.pop.umn.edu/images/043.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach12", CatPictureUrl = "http://pv.pop.umn.edu/images/009.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach13", CatPictureUrl = "http://pv.pop.umn.edu/images/021.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach14", CatPictureUrl = "http://pv.pop.umn.edu/images/051.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach15", CatPictureUrl = "http://pv.pop.umn.edu/images/041.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach16", CatPictureUrl = "http://pv.pop.umn.edu/images/012.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach17", CatPictureUrl = "http://pv.pop.umn.edu/images/096.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach18", CatPictureUrl = "http://pv.pop.umn.edu/images/077.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach19", CatPictureUrl = "http://pv.pop.umn.edu/images/069.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach20", CatPictureUrl = "http://pv.pop.umn.edu/images/008.jpg", DateCreated = DateTime.Now},
                new Winner() {Username = "buddyroach21", CatPictureUrl = "http://pv.pop.umn.edu/images/034.jpg", DateCreated = DateTime.Now},
            };

            winners.ForEach(s => context.Winners.Add(s));

            context.SaveChanges();
        }
    }
}
