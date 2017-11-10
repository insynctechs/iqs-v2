using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using IQSCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.Net.Mail;
//using IQSCore.Helpers;


namespace IQSCore.Controllers
{
    public class ReviewsController : ApiController
    {


        Review rev = new Review();

        public class RootObject
        {
            public List<string> list { get; set; }
            public string doaction { get; set; }
            public string returntype { get; set; }
        }

        [Route("api/Reviews/TagReviewHelpful")]
        [HttpGet]
        public async Task<IHttpActionResult> TagReviewHelpful(int CommentId, int json=0)
        {
            var res = await rev.TagReviewHelpful(CommentId);
            if(json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else 
                return Ok(res);
           
        }

        [Route("api/Reviews/UpdateReviewRating")]
        [HttpGet]
        public async Task<IHttpActionResult> UpdateReviewRating(int CommentId, int Rate, int json=0)
        {
            var res = await rev.UpdateReviewRating(CommentId, Rate);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetReviews")]
        public async Task<IHttpActionResult> GetReviews(int Client_SK, int LastCommentId, int json = 0)
        {
            var res = await rev.GetReviews(Client_SK, LastCommentId);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetReviewReplies")]
        public async Task<IHttpActionResult> GetReviewReplies(int CommentId, int json=0)
        {
            var res = await rev.GetReviewReplies(CommentId);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetProfanity")]
        public async Task<IHttpActionResult> GetProfanity(string word, int json=0)
        {
            var res = await rev.GetProfanity(word);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/InsertCommenter")]
        [HttpGet]
        public async Task<IHttpActionResult> InsertCommenter(string DesiredName, string FullName, string Email, string Password, string SystemIp, int Active, int json = 0)
        {
            var res = await rev.InsertCommenter(DesiredName, FullName, Email, Password, SystemIp, Active);
            return Ok(res);

        }

        /*[Route("api/Reviews/AddCommenter")]
        [HttpPost]
        public async Task<IHttpActionResult> AddCommenter([FromBody] JObject jData)
        {
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(jData.ToString());
            object[] objData = obj.list.ToArray();
            var res = await rev.InsertCommenter(objData[0].ToString(), objData[1].ToString(), objData[2].ToString(), objData[3].ToString(), objData[4].ToString(), 1);
            if (res == "success")
            {
                if (obj.doaction == "yes")
                    ReviewActions.SendRegistrationMail(objData[1].ToString(), objData[2].ToString(), objData[3].ToString());

            }
            if(obj.returntype == "json")
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);
        }
        */



        [Route("api/Reviews/GetCommentersLogin")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCommentersLogin(string Email, string Password, int json = 0)
        {
            var res = await rev.GetCommentersLogin(Email, Password);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetFBCommentersLogin")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFBCommentersLogin(string DesiredName, string FullName, string Email, string Password, string SystemIp, int Active, int json = 0)
        {
            var res = await rev.FBCommentersLogin(DesiredName, FullName, Email, Password, SystemIp, Active);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/WriteReview")]
        [HttpGet]
        public async Task<IHttpActionResult> WriteReview(int UserId, int Rating, string Title, string Review, int Client_SK, int json = 0)
        {
            var res = await rev.WriteReview(UserId, Rating, Title, Review, Client_SK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/WriteReviewReply")]
        [HttpGet]
        public async Task<IHttpActionResult> WriteReviewReply(int UserId, int CommentId, string Review, string CommentType, int json = 0)
        {
            var res = await rev.WriteReviewReply(UserId, CommentId, Review, CommentType);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/InsertSystemIp")]
        [HttpGet]
        public async Task<IHttpActionResult> InsertSystemIp(string SystemIp, int json = 0)
        {
            var res = await rev.InsertSystemIp(SystemIp);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetSystemIp")]
        public async Task<IHttpActionResult> GetSystemIp(string SystemIp, int json = 0)
        {
            var res = await rev.GetSystemIp(SystemIp);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/DisableCommenter")]
        public async Task<IHttpActionResult> DisableCommenter(int UserId, int json = 0)
        {
            var res = await rev.DisableCommenter(UserId);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetCommenterActiveValue")]
        public async Task<IHttpActionResult> GetCommenterActiveValue(int UserId, int json = 0)
        {
            var res = await rev.GetCommenterActiveValue(UserId);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetCommenterByEmail")]
        public async Task<IHttpActionResult> GetCommenterByEmail(string Email, int json = 0)
        {
            var res = await rev.GetCommenterByEmail(Email);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetCompanyTotalRating")]
        public async Task<IHttpActionResult> GetCompanyTotalRating(int ClientSK, int json = 0)
        {
            var res = await rev.GetCompanyTotalRating(ClientSK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/GetCompanyRatingByArray")]
        public async Task<IHttpActionResult> GetCompanyRatingByArray(string ClientSkArray, int json = 0)
        {
            var res = await rev.GetCompanyRatingByArray(ClientSkArray);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        
        







    }
}
