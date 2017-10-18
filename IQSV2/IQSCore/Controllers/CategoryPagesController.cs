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
    public class CategoryPagesController : ApiController
    {
        CategoryPages categoryPages = new CategoryPages();

        

        // GET: api/CategoryPages
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/CategoryPages/GetCategoryList")]
        public async Task<IHttpActionResult> GetCategoryList(int json=0)
        {
            var cat = await categoryPages.GetCategoryList();
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/CategoryPages/GetCategoryPage1Details")]
        public async Task<IHttpActionResult> GetCategoryPage1Details(int CategorySK, string WebsiteType, int json=0)
        {
            var cat = await categoryPages.GetCategoryPage1Details(CategorySK, WebsiteType);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/CategoryPages/GetCategoryIdByName")]
        public async Task<IHttpActionResult> GetCategoryIdByName(string DisplayName, int json=0)
        {
            var cat = await categoryPages.GetCategoryIdByName(DisplayName);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/CategoryPages/GetCategoryPage2Details")]
        public async Task<IHttpActionResult> GetCategoryPage2Details(int CategorySK, string WebsiteType, int json=0)
        {
            var cat = await categoryPages.GetCategoryPage2Details(CategorySK, WebsiteType);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        [Route("api/CategoryPages/GetCategoryStateValidate")]
        public async Task<IHttpActionResult> GetCategoryStateValidate(string Category, string State, int json=0)
        {
            var cat = await categoryPages.GetCategoryStateValidate(Category, State);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(cat, Formatting.Indented));
            else
                return Ok(cat);
        }

        // GET: api/CategoryPages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CategoryPages
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CategoryPages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CategoryPages/5
        public void Delete(int id)
        {
        }
    }
}
