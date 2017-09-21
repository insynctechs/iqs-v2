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
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@SrhStr", SrhStr);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetSearchResults", sqlParam));
        }

        public async Task<DataSet> GetStateForSearch(string CategorySK)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@CATEGORY_SK", CategorySK);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetStateForSearch", sqlParam));
        }
    }
}