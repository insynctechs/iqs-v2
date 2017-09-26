using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IQSCore.Models;

namespace IQSCore.Controllers
{
    public class ArticlesController : ApiController
    {
        Articles articles = new Articles();

        [Route("api/Articles/GetLatestArticles")]
        public async Task<IHttpActionResult> GetLatestArticles(string CategoryName)
        {
            var art = await articles.GetLatestArticles(CategoryName);
            return Ok(art);
        }
    }
}
