using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IQSCore.Models
{
    public class CategoryPages
    {
        public async Task<DataSet> GetCategoryList()
        {
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "Usp_GetCategoryList"));
        }

        public async Task<DataSet> GetCategoryPage1Details(int CategorySK, string WebsiteType)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@CategorySK", CategorySK);
            sqlParam[1] = new SqlParameter("@WebSiteType", WebsiteType);
            sqlParam[2] = new SqlParameter("@DirectoryWebsiteURL", "");
            sqlParam[3] = new SqlParameter("@Page2AdsCount", SqlDbType.Int);
            sqlParam[3].Direction = ParameterDirection.Output;
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetPage1AdvertisementDetails", sqlParam));
        }

        public async Task<DataSet> GetCategoryIdByName(string DisplayName)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@Display_Name", DisplayName);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCategoryIdByName", sqlParam));
        }

        public async Task<DataSet> GetCategoryPage2Details(int CategorySK, string WebsiteType)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@CategorySK", CategorySK);
            sqlParam[1] = new SqlParameter("@WebSiteType", WebsiteType);
            sqlParam[2] = new SqlParameter("@DirectoryWebsiteURL", "");
            sqlParam[3] = new SqlParameter("@Page3AdsCount", SqlDbType.Int);
            sqlParam[3].Direction = ParameterDirection.Output;
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetPage2AdvertisementDetails", sqlParam));
        }

        public async Task<DataSet> GetCategoryStateValidate(string Category, string State)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@Category", Category);
            sqlParam[1] = new SqlParameter("@State", State);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspCategoryStateValidate", sqlParam));
        }

    }
}