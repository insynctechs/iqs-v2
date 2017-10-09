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
    public class RFQController : ApiController
    {
        RFQ rfq = new RFQ();

        [Route("api/RFQ/GetCategoryDisplayName")]
        public async Task<IHttpActionResult> GetCategoryDisplayName(int CategorySK)
        {
            var cat = await rfq.GetCategoryDisplayName(CategorySK);
            return Ok(cat);
        }

        [Route("api/RFQ/GetCompanyDetailsForCategory")]
        public async Task<IHttpActionResult> GetCompanyDetailsForCategory(int CategorySK)
        {
            var cat = await rfq.GetCategoryDisplayName(CategorySK);
            return Ok(cat);
        }

        [Route("api/RFQ/GetCompanyDetailsForClient")]
        public async Task<IHttpActionResult> GetCompanyDetailsForClient(int ClientSK)
        {
            var cat = await rfq.GetCategoryDisplayName(ClientSK);
            return Ok(cat);
        }
        [Route("api/RFQ/InsertRFQ")]
        public async Task<IHttpActionResult> InsertRFQ(int CategorySK, string CompanyName, string ContactName, string Email, string Address, string Phone, string Comments, string RequestIP)
        {
            
            var cat = await rfq.InsertRFQ(CategorySK, CompanyName, ContactName, Email, Address, Phone, Comments, RequestIP);
            return Ok(cat);
        }

        [Route("api/RFQ/InsertRFQClientDetails")]
        public async Task<IHttpActionResult> InsertRFQClientDetails(int RFQHeaderSK, int ClientSK, int SequenceNo, int TierSK)
        {

            var cat = await rfq.InsertRFQClientDetails(RFQHeaderSK, ClientSK, SequenceNo, TierSK);
            return Ok(cat);
        }
    }
}
