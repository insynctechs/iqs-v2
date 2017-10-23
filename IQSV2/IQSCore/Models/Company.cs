using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
    }
}