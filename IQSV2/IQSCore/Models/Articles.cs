using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IQSCore.Models
{
    public class Articles
    {
        public async Task<DataSet> GetLatestArticles(string CategoryName)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@Category", CategoryName);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetLatestArticles", sqlParam));
        }

        public async Task<DataSet> GetArticlesByClientId(int Client_SK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CLIENT_ID", Client_SK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetArticleByClientId", sqlParam));
        }
    }
}