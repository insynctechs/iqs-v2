using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace IQSCore.Models
{
    public class RFQ
    {
        #region " Load Category name and Display name "
        public async Task<DataSet> GetCategoryDisplayName(int CategorySK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CATEGORYSK", CategorySK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCategoryNameandDisplayName", sqlParam));
        }
        #endregion

        #region " Load Company Details "
        public async Task<DataSet> GetCompanyDetailsForCategory(int CategorySK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CATEGORY_SK", CategorySK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetRFQCompanyDetails", sqlParam));
        }
        #endregion

        #region " Load Company Details By Id"
        public async Task<DataSet> GetCompanyDetailsForClient(int ClientSK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENT_SK", ClientSK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetRFQCompanyDetailsById", sqlParam));
        }
        #endregion

        #region "Insert RFQ Details"
        public async Task<int> InsertRFQ(int CategorySK, string CompanyName, string ContactName, string Email, string Address, string Phone, string Comments, string RequestIP)
        {
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@CATEGORY_SK", CategorySK);
            sqlParam[1] = new SqlParameter("@COMPANY_NAME", CompanyName);
            sqlParam[2] = new SqlParameter("@CONTACT_NAME", ContactName);
            sqlParam[3] = new SqlParameter("@EMAIL", Email);
            sqlParam[4] = new SqlParameter("@ADDRESS", Address);
            sqlParam[5] = new SqlParameter("@PHONE", Phone);
            sqlParam[6] = new SqlParameter("@COMMENTS", Comments);
            sqlParam[7] = new SqlParameter("@REQUESTIP", RequestIP);
            sqlParam[8] = new SqlParameter("@RETURNVALUE", SqlDbType.Int);
            sqlParam[8].Direction = ParameterDirection.Output;
            sqlParam[8].Size = 10;
            await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspInsertRFQDetails", sqlParam));
            return Int32.Parse(sqlParam[8].Value.ToString());
        }
        #endregion

        #region "Insert RFQ Client Details"
        public async Task<int> InsertRFQClientDetails(int RFQHeaderSK, int ClientSK, int SequenceNo, int TierSK)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@RFQHEADER_SK", RFQHeaderSK);
            sqlParam[1] = new SqlParameter("@CLIENT_SK", ClientSK);
            sqlParam[2] = new SqlParameter("@SEQUENCE_NO", SequenceNo);
            sqlParam[3] = new SqlParameter("@TIER_SK", TierSK);
            return await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspInsertRFQClient", sqlParam));
        }
        #endregion
    }
}