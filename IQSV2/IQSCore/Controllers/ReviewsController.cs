using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using IQSCore.Models;
using Newtonsoft.Json;

namespace IQSCore.Controllers
{
    public class ReviewsController : ApiController
    {


        Review rev = new Review();

        [Route("api/Reviews/TagReviewHelpful")]
        public async Task<IHttpActionResult> TagReviewHelpful(int CommentId, int json=0)
        {
            var res = await rev.TagReviewHelpful(CommentId);
            if(json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else 
                return Ok(res);
           
        }

        [Route("api/Reviews/UpdateReviewRating")]
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
        public async Task<IHttpActionResult> InsertCommenter(string DesiredName, string FullName, string Email, string Password, string SystemIp, bool Active, int json = 0)
        {
            var res = await rev.InsertCommenter(DesiredName, FullName, Email, Password, SystemIp, Active);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/CommentersLogin")]
        public async Task<IHttpActionResult> CommentersLogin(string Email, string Password, int json = 0)
        {
            var res = await rev.CommentersLogin(Email, Password);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/InsertCommenter")]
        public async Task<IHttpActionResult> FBCommentersLogin(string DesiredName, string FullName, string Email, string Password, string SystemIp, bool Active, int json = 0)
        {
            var res = await rev.FBCommentersLogin(DesiredName, FullName, Email, Password, SystemIp, Active);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/WriteReview")]
        public async Task<IHttpActionResult> WriteReview(int UserId, int Rating, string Title, string Review, int Client_SK, int json = 0)
        {
            var res = await rev.WriteReview(UserId, Rating, Title, Review, Client_SK);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/WriteReviewReply")]
        public async Task<IHttpActionResult> WriteReviewReply(int UserId, int CommentId, string Review, string CommentType, int json = 0)
        {
            var res = await rev.WriteReviewReply(UserId, CommentId, Review, CommentType);
            if (json == 1)
                return Json(JsonConvert.SerializeObject(res, Formatting.Indented));
            else
                return Ok(res);

        }

        [Route("api/Reviews/InsertSystemIp")]
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

        [Route("api/Reviews/CommenterActiveCheck")]
        public async Task<IHttpActionResult> CommenterActiveCheck(int UserId, int json = 0)
        {
            var res = await rev.CommenterActiveCheck(UserId);
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
