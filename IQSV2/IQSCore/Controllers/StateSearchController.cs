using IQSCore.Models;
using Newtonsoft.Json;
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
            SrhStr = JsonConvert.DeserializeObject(SrhStr).ToString();
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
                //List<DataRow> srhRes = ds.Tables[0].AsEnumerable().ToList();
                List<string> result = (from res in ds.Tables[0].AsEnumerable()
                                       select res["NAME"].ToString()).ToList();
                return Json(JsonConvert.SerializeObject(result));
            }
        }

        [Route("api/StateSearch/GetStateForSearch")]
        public async Task<IHttpActionResult> GetStateForSearch(string CategorySK)
        {
            CategorySK = JsonConvert.DeserializeObject(CategorySK).ToString();
            DataSet ds = await stateSearch.GetStateForSearch(CategorySK);
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
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = "";
                dr[1] = "All States";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                List<object> lo = new List<object>();
                foreach(DataRow d in ds.Tables[0].Rows)
                    lo.Add(new object[] { d[0].ToString(), d[1].ToString() });
                return Json(JsonConvert.SerializeObject(lo));
            }
        }
    }
}
