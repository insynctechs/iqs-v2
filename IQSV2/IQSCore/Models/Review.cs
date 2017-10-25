using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IQSCore.Models
{
    public class Review
    {
        #region " Tag A Review Comment as Helpful "
        public async Task<string> TagReviewHelpful(int CommentId)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CommentId", CommentId);
            DataSet ds = await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspUpdateCommentHelpful", sqlParam));
            if (ds.Tables.Count == 0)
                return "invalid";
            else if (ds.Tables[0].Rows.Count == 0)
                return "invalid";
            else
                return ds.Tables[0].Rows[0][0].ToString();

        }
        #endregion

        #region " Update Rating for a Review "
        public async Task<string> UpdateReviewRating(int CommentId, int Rate)
        {
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@CommentId", CommentId);
            sqlParam[1] = new SqlParameter("@RateReceived", Rate);
            sqlParam[2] = new SqlParameter("@RetVal", SqlDbType.VarChar);
            sqlParam[2].Direction = ParameterDirection.Output;
            await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspUpdateReviewRating", sqlParam));
            return sqlParam[2].Value.ToString();
        }
        #endregion
        
        #region " Get Reviews "
        public async Task<DataSet> GetReviews(int Client_SK, int LastCommentId)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@Client_SK", Client_SK);
            sqlParam[1] = new SqlParameter("@LastCommentId", LastCommentId);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetComments", sqlParam));
            
        }
        #endregion

        #region " Get Replies to a Review "
        public async Task<DataSet> GetReviewReplies(int CommentId)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CommentId", CommentId);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetSubComments", sqlParam));

        }
        #endregion

        #region " Get Profanity "
        public async Task<DataSet> GetProfanity(string word)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@ProfanityWord", word);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetProfanity", sqlParam));

        }
        #endregion

        #region " Insert Commenters "
        public async Task<string> InsertCommenter(string DesiredName, string FullName, string Email, string Password, string SystemIp, int Active)
        {
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@DesiredName", DesiredName);
            sqlParam[1] = new SqlParameter("@FullName", FullName);
            sqlParam[2] = new SqlParameter("@Email", Email);
            sqlParam[3] = new SqlParameter("@Password", Password);
            sqlParam[4] = new SqlParameter("@SystemIp", SystemIp);
            sqlParam[5] = new SqlParameter("@Active", Convert.ToBoolean(Active));
            sqlParam[6] = new SqlParameter("@RetVal", SqlDbType.VarChar, 200);
            sqlParam[6].Direction = ParameterDirection.Output;
            await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspInsertCommenters", sqlParam));
            return sqlParam[6].Value.ToString();
        }
        #endregion        

        #region " Login Commenters "
        public async Task<DataSet> GetCommentersLogin(string Email,string Password)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@Email", Email);
            sqlParam[1] = new SqlParameter("@Password", Password);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspCommentersLogin", sqlParam));

        }
        #endregion

        #region " Login FB Commenters "
        public async Task<DataSet> FBCommentersLogin(string DesiredName, string FullName, string Email, string Password, string SystemIp, int Active)
        {
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@DesiredName", DesiredName);
            sqlParam[1] = new SqlParameter("@FullName", FullName);
            sqlParam[2] = new SqlParameter("@Email", Email);
            sqlParam[3] = new SqlParameter("@Password", Password);
            sqlParam[4] = new SqlParameter("@SystemIp", SystemIp);
            sqlParam[5] = new SqlParameter("@Active", Active);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspFBCommentersLogin", sqlParam));

        }
        #endregion

        #region " Write Reviews "
        public async Task<DataSet> WriteReview(int UserId, int Rating, string Title, string Review, int Client_SK)
        {
            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@UserId", UserId);
            sqlParam[1] = new SqlParameter("@Rating", Rating);
            sqlParam[2] = new SqlParameter("@Title", Title);
            sqlParam[3] = new SqlParameter("@Review", Review);
            sqlParam[4] = new SqlParameter("@Client_SK", Client_SK);            
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspInsertComments", sqlParam));

        }
        #endregion

        #region " Write Review Reply "
        public async Task<DataSet> WriteReviewReply(int UserId, int CommentId, string Review, string CommentType)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@UserId", UserId);
            sqlParam[1] = new SqlParameter("@CommentId", CommentId);
            sqlParam[2] = new SqlParameter("@Review", Review);
            sqlParam[3] = new SqlParameter("@CommentType", CommentType);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspInsertSubComments", sqlParam));

        }
        #endregion

        #region " Insert System IP "
        public async Task<string> InsertSystemIp(string SystemIp)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[1] = new SqlParameter("@SystemIp", SystemIp);
            int res = await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspInsertCommenterPriv", sqlParam));
            if (res > 0)
                return "success";
            else
                return "failure";
        }
        #endregion

        #region " Get System IP "
        public async Task<DataSet> GetSystemIp(string SystemIp)
        {
           
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[1] = new SqlParameter("@SystemIp", SystemIp);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCommenterPriv", sqlParam));

        }
        #endregion

        #region " Disable Commenter "
        public async Task<string> DisableCommenter(int UserId)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[1] = new SqlParameter("@UserId", UserId);
            int res = await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspDisableCommenter", sqlParam));
            if (res > 0)
                return "success";
            else
                return "failure";
        }
        #endregion

        #region " Commenter Active Check "
        public async Task<int> GetCommenterActiveValue(int UserId)
        {

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[1] = new SqlParameter("@UserId", UserId);
            DataSet ds =  await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCommenterIsActive", sqlParam));
            if (ds.Tables.Count == 0)
                return -1;
            else if(ds.Tables[0].Rows.Count == 0)
                return -1;
            else 
                return Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
        }
        #endregion

        #region " Get Commenter By Email "
        public async Task<DataSet> GetCommenterByEmail(string Email)
        {

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[1] = new SqlParameter("@Email", Email);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCommenterByEmail", sqlParam));
           
        }
        #endregion

        #region " Company Total Rating "
        public async Task<DataSet> GetCompanyTotalRating(int ClientSK)
        {

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[1] = new SqlParameter("@ClientSK", ClientSK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCompanyTotalRating", sqlParam));

        }
        #endregion

        #region "Get Company TotalRating ByArray"
        public async Task<DataSet> GetCompanyRatingByArray(string ClientSkArray)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@ClientSkArray", ClientSkArray);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCompanyTotalRatingByArray", sqlParam));
        }
        #endregion
    }
}