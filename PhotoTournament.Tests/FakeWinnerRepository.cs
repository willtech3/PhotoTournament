using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoTournament.Data;
using PhotoTournament.Model;

namespace PhotoTournament.Tests
{
    public class FakeWinnerRepository : EFRepository<Winner>, IWinnerRepository 
    {
        public FakeWinnerRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Winner> GetLastTenWinners()
        {
            var list = new List<Winner>()
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
                new Winner() {Username = "buddyroach11", CatPictureUrl = "http://pv.pop.umn.edu/images/043.jpg", DateCreated = DateTime.Now}
            };
            return list;
        }

        public Winner GetLatestWinnerByUsername(string username)
        {
            return new Winner()
            {
                Username = "buddyroach1",
                CatPictureUrl = "http://pv.pop.umn.edu/images/079.jpg",
                DateCreated = DateTime.Now
            };
        }

        public override IQueryable<Winner> GetAll()
        {
            var list = this.GetLastTenWinners().AsQueryable();
            return list;
        }

        //hides the method in EFRepository
        public new void Add(Winner entity)
        {
        }
    }
}
