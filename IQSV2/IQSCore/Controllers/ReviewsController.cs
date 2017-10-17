using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using IQSCore.Models;

namespace IQSCore.Controllers
{
    public class ReviewsController : ApiController
    {


        Review rev = new Review();

        [Route("api/Reviews/TagReviewHelpful")]
        public async Task<IHttpActionResult> TagReviewHelpful(int CommentId)
        {
            var res = await rev.TagReviewHelpful(CommentId);
            return Ok(res);
        }

        [Route("api/Reviews/UpdateReviewRating")]
        public async Task<IHttpActionResult> UpdateReviewRating(int CommentId, int Rate)
        {
            var res = await rev.UpdateReviewRating(CommentId, Rate);
            return Ok(res);
        }



    }
}
