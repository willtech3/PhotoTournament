using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PhotoTournament.Data;
using PhotoTournament.Model;
using PhotoTournament.Models;

namespace PhotoTournament.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected IPhotoTournamentUow Uow { get; set; }
    }
    [System.Web.Mvc.Authorize]
    public class WinnerController : ApiControllerBase
    {

        private ApplicationUserManager _userManager;
        public WinnerController(IPhotoTournamentUow uow)
        {
            Uow = uow;
        }

        [HttpGet]
        public PagedResults GetAllWinners(int page = 0, int pageSize = 10)
        {
            var query = Uow.Winners.GetAll().OrderByDescending(s => s.DateCreated);
            var totalCount = query.Count();
            var totalPages = Math.Ceiling((double) totalCount/pageSize);

            var results = query.Skip(pageSize * page)
                               .Take(pageSize)
                               .ToList();
            
            return new PagedResults()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                Results = results
            };
        }
        public string GetWinner()
        {
            var username = User.Identity.Name;
            return Uow.Winners.GetLatestWinnerByUsername(username).CatPictureUrl;
        }

        [HttpPost]
        public virtual void SaveWinner(UrlModel model)
        {
            if (model.Url.IsNullOrWhiteSpace())
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            var winner = new Winner() { Username=User.Identity.Name, DateCreated = DateTime.Now, CatPictureUrl = model.Url };
            Uow.Winners.Add(winner);
            Uow.Commit();
        }

        public class UrlModel
        {
            public string Url { get; set; }
        }

        public class PagedResults
        {
            public int TotalCount { get; set; }
            public double TotalPages { get; set; }
            public int CurrentPage { get; set; }
            public IList<Winner> Results { get; set; }
        }
    }
}
