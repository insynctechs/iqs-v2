using IQSCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IQSCore.Controllers
{
    public class CompanyRatingsController : ApiController
    {
        CompanyRatings companyRatings = new CompanyRatings();

        [Route("api/CompanyRatings/GetCompanyRatingByArray")]
        public async Task<IHttpActionResult> GetCompanyRatingByArray(string ClientSkArray)
        {
            var crate = await companyRatings.GetCompanyRatingByArray(ClientSkArray);
            return Ok(crate);
        }

        // GET: api/CompanyRatings
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CompanyRatings/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CompanyRatings
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CompanyRatings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CompanyRatings/5
        public void Delete(int id)
        {
        }
    }
}
