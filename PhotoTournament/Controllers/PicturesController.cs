using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using PhotoTournament.Data;
using WebGrease.Css.Extensions;

namespace PhotoTournament.Controllers
{
    public class PicturesController : ApiControllerBase
    {
        protected IPhotoTournamentUow Uow { get; set; }


        public class RootObject
        {
            public string Status { get; set; }
            public int code { get; set; }
            public List<String> data { get; set; }
        }

        [HttpGet]
        public List<string> GetPhotoUrls()
        {
            var request = WebRequest.Create("http://pv.pop.umn.edu/images");
            var response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var obj = JsonConvert.DeserializeObject<RootObject>(responseFromServer);
            reader.Close();
            response.Close();

            return obj.data;
        }
    }
}
