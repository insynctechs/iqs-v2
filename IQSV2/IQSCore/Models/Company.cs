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

    }
}