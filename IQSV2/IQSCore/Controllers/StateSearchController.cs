using IQSCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IQSCore.Controllers
{
    public class StateSearchController : ApiController
    {
        StateSearch stateSearch = new StateSearch();

        [Route("api/StateSearch/GetSearchResults")]
        public async Task<IHttpActionResult> GetSearchResults(string SrhStr)
        {
            DataSet ds = await stateSearch.GetSearchResults(SrhStr);
            if (ds == null)
            {
                return Ok("Invalid");
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                return Ok("Invalid");
            }
            else
            {
                List<DataRow> srhRes = ds.Tables[0].AsEnumerable().ToList();
                List<string> result = (from res in srhRes
                                      select res["NAME"].ToString()).ToList();
                return Ok(result);
            }
        }
    }
}
