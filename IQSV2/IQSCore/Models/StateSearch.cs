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

        public async Task<DataSet> GetSearchResultsDetails(string SrhStr, string Start, string Count, string State)
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@SrhStr", SrhStr);
            sqlParam[1] = new SqlParameter("@Start", Start);
            sqlParam[2] = new SqlParameter("@Count", Count);
            sqlParam[3] = new SqlParameter("@State", State);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspGetSearchResultsDetails", sqlParam));
        }

        public async Task<DataSet> StateSearchURLValidate(string category, string state, string country=null, string category1=null, string category2=null, string city=null)
        {
            SqlParameter[] sqlParam = new SqlParameter[6];
            sqlParam[0] = new SqlParameter("@Category", category);
            sqlParam[1] = new SqlParameter("@State", state);
            sqlParam[2] = new SqlParameter("@Country", country);
            sqlParam[3] = new SqlParameter("@Category1", category1);
            sqlParam[4] = new SqlParameter("@Category2", category2);
            sqlParam[5] = new SqlParameter("@City", city);
            return await Task.Run(() => SqlHelper.ExecuteDataset(Settings.Constr, CommandType.StoredProcedure, "uspStateSearchURLValidate", sqlParam));
        }
    }
}