using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using IQSCore.Models;

namespace IQSCore.Controllers
{
    public class CompanyProfileController : ApiController
    {
        CompanyProfile companyProfile = new CompanyProfile();

        [Route("api/CompanyProfile/GetCompanyProfileById")]
        public async Task<IHttpActionResult> GetCompanyProfileById(int Client_SK, int json = 0)
        {
            var cat = await companyProfile.GetCompanyProfileById(Client_SK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }
    }
}
