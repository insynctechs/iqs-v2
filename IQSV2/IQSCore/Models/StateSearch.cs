using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IQSCore.Models
{
    public class StateSearch
    {
        public async Task<DataSet> GetSearchResults(string SrhStr)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@SrhStr", SrhStr);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetSearchResults", sqlParam));
        }

        public async Task<DataSet> GetStateForSearch(string CategorySK)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@CATEGORY_SK", CategorySK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetStateForSearch", sqlParam));
        }

        public async Task<DataSet> GetStateSearchResults(string Category, string State)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@Category", Category);
            sqlParam[1] = new SqlParameter("@State", State);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetStateSearchResults", sqlParam));
        }

        public async Task<DataSet> GetStateSearchPageDetails(string Category, string State)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@Category", Category);
            sqlParam[1] = new SqlParameter("@State", State);
            sqlParam[2] = new SqlParameter("@WebsiteType", "directory");
            sqlParam[3] = new SqlParameter("@COUNTRYNAME", "Canada");
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetStateSearchPageDetails", sqlParam));
        }
    }
}