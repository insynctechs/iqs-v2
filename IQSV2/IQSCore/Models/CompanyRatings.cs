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
    public class CompanyRatings
    {
        public async Task<DataSet> GetCompanyRatingByArray(string ClientSkArray)
        {
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@ClientSkArray", ClientSkArray);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetCompanyTotalRatingByArray", sqlParam));
        }
    }
}