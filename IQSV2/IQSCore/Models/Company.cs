using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;
namespace IQSCore.Models
{
    public class Company
    {
        public async Task<DataSet> GetClientNameEmailById(int ClientSK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENT_SK", ClientSK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetClientNameForPageEmail", sqlParam));
        }

        public async Task<DataSet> GetCompanyProfileById(int Client_SK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENT_SK", Client_SK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetClientDetailsByClientSk", sqlParam));
        }

        public async Task<DataSet> GetClientProfileDetails(int Client_SK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENTSK", Client_SK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetClientProfileDetails", sqlParam));
        }

        public async Task<DataSet> GetListCompanies(string SrhLetter)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@SrhLetter", SrhLetter);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetListCompanies", sqlParam));
        }

        #region "Insert DirectoryProfileEmailDetails"
        public async Task<int> InsertDirectoryProfileEmailDetails(String FirstName, String LastName, String EmailAddress, string CompanyName, string Zip, string Subject, string Message, int ClientSk, string RequestIp)
        {
            SqlParameter[] sqlParam = new SqlParameter[13];
            sqlParam[0] = new SqlParameter("@FIRSTNAME", FirstName);
            sqlParam[1] = new SqlParameter("@LASTNAME", LastName);
            sqlParam[2] = new SqlParameter("@EMAIL_ADDRESS",EmailAddress);
            sqlParam[3] = new SqlParameter("@ZIP", Zip);
            sqlParam[4] = new SqlParameter("@SUBJECT", Subject);
            sqlParam[5] = new SqlParameter("@MESSAGE", Message);
            sqlParam[6] = new SqlParameter("@COMPANY",CompanyName);
            sqlParam[7] = new SqlParameter("@CLIENTSK", ClientSk);
            sqlParam[8] = new SqlParameter("@CATEGORYSK", 0);
            sqlParam[9] = new SqlParameter("@REQUEST_TYPE_SK", 0);
            sqlParam[10] = new SqlParameter("@WEBSITE_TYPE", "DIRECTORY");
            sqlParam[11] = new SqlParameter("@REQUESTIP", RequestIp);
            sqlParam[12] = new SqlParameter("@OutParam", SqlDbType.Int);
            sqlParam[12].Direction = ParameterDirection.Output;
            sqlParam[12].Size = 10;
            await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspInsertDirectoryProfileEmailDetails", sqlParam));
            return Convert.ToInt32(sqlParam[12].Value);
        }
        #endregion

        #region "Insert Company ProfileEmailDetails"
        public async Task<int> InsertCompanyProfileEmailDetails(String CompanyName, String FirstName, String LastName, String EmailAddress, string Phone, string City, string Subject, string Message, int ClientSk, string RequestIp)
        {
            SqlParameter[] sqlParam = new SqlParameter[14];
            sqlParam[0] = new SqlParameter("@FIRSTNAME",FirstName);
            sqlParam[1] = new SqlParameter("@LASTNAME", LastName);
            sqlParam[2] = new SqlParameter("@EMAIL_ADDRESS", EmailAddress);
            sqlParam[3] = new SqlParameter("@PHONE", Phone);
            sqlParam[4] = new SqlParameter("@CITY", City);
            sqlParam[5] = new SqlParameter("@SUBJECT", Subject);
            sqlParam[6] = new SqlParameter("@MESSAGE", Message);
            sqlParam[7] = new SqlParameter("@COMPANY", CompanyName);
            sqlParam[8] = new SqlParameter("@CLIENTSK", ClientSk);
            sqlParam[9] = new SqlParameter("@CATEGORYSK", 0);
            sqlParam[10] = new SqlParameter("@REQUEST_TYPE_SK", 0);
            sqlParam[11] = new SqlParameter("@WEBSITE_TYPE", "DIRECTORY");
            sqlParam[12] = new SqlParameter("@REQUESTIP", RequestIp);
            sqlParam[13] = new SqlParameter("@OutParam", SqlDbType.Int);
            sqlParam[13].Direction = ParameterDirection.Output;
            sqlParam[13].Size = 10;
            await Task.Run(() => SqlHelper.ExecuteNonQuery(Settings.Constr, CommandType.StoredProcedure, "uspInsertCompanyProfileEmailDetails", sqlParam));
            return Convert.ToInt32(sqlParam[13].Value);
        }
        #endregion

        #region "Get Client Details By Name"
        public async Task<DataSet> GetClientDetailsByName(string CompanyName)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENT_NAME", CompanyName);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetClientDetailsByClientName", sqlParam));

        }
        #endregion

        #region "Get Client ID By Name"
        public async Task<DataSet> GetClientIdByName(string CompanyName)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENT_NAME", CompanyName);            
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetClientIdByClientName", sqlParam));

        }
        #endregion
    }
}