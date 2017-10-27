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
    public class CompaniesController : ApiController
    {
        Company comp = new Company();

        [Route("api/Clients/GetClientNameEmailById")]
        public async Task<IHttpActionResult> GetClientNameEmailById(int ClientSK, int json = 0)
        {
            var res = await comp.GetClientNameEmailById(ClientSK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);
        }

        [Route("api/Clients/GetCompanyProfileById")]
        public async Task<IHttpActionResult> GetCompanyProfileById(int Client_SK, int json = 0)
        {
            var cat = await comp.GetCompanyProfileById(Client_SK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/Clients/GetClientProfileDetails")]
        public async Task<IHttpActionResult> GetClientProfileDetails(int Client_SK, int json = 0)
        {
            var cat = await comp.GetClientProfileDetails(Client_SK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/Clients/InsertDirectoryProfileEmailDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> InsertDirectoryProfileEmailDetails(String FirstName, String LastName, String EmailAddress, string CompanyName, string Zip, string Subject, string Message, int ClientSk, string RequestIp)
        {
            var cat = await comp.InsertDirectoryProfileEmailDetails(FirstName, LastName, EmailAddress, CompanyName, Zip, Subject, Message, ClientSk, RequestIp);
            return Ok(cat);
        }

        
    }
}
