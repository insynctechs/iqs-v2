using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using IQSCore.Models;
using IQSCore.Helpers;

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

        [Route("api/Clients/GetListCompanies")]
        public async Task<IHttpActionResult> GetListCompanies(string SrhLetter, int json = 0)
        {
            var cat = await comp.GetListCompanies(SrhLetter);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/Clients/InsertDirectoryProfileEmailDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> InsertDirectoryProfileEmailDetails(String CompanyName, String Name, String EmailAddress, string Phone, string City, string Subject, string Message, int ClientSk, string RequestIp)
        {
             
            var cat = await comp.InsertDirectoryProfileEmailDetails(CompanyName, Name, EmailAddress, Phone, City, Subject, Message, ClientSk, RequestIp);
            return Ok(cat);
        }

        [Route("api/Clients/InsertCompanyProfileEmailDetails")]
        [HttpGet]
        public async Task<IHttpActionResult> InsertCompanyProfileEmailDetails(String CompanyName, String FirstName, String LastName, String EmailAddress, string Phone, string City, string Subject, string Message, int ClientSk, string RequestIp)
        {

            var cat = await comp.InsertCompanyProfileEmailDetails(CompanyName, FirstName, LastName, EmailAddress, Phone, City, Subject, Message, ClientSk, RequestIp);
            return Ok(cat);
        }

        [Route("api/Clients/GetClientDetailsByName")]
        public async Task<IHttpActionResult> GetClientDetailsByName(string CName, string key="",int json = 0)
        {
            if (key == System.Configuration.ConfigurationManager.AppSettings["APIKey"].ToString())
            {
                var res = await comp.GetClientDetailsByName(CName);
                if (json == 1)
                    return Json(ApiUtils.ConvertDataSetToJsonString(res));
                else
                    return Ok(res);
            }
            else
                return Json("Invalid Key");
        }

        [Route("api/Clients/GetClientIdByName")]
        public async Task<IHttpActionResult> GetClientIdByName(string CName, string key="", int json = 0)
        {
            var res = await comp.GetClientIdByName(CName);
            if (json == 1)
                return Json(ApiUtils.ConvertDataSetToJsonString(res));
            else
                return Ok(res);
        }


    }
}
