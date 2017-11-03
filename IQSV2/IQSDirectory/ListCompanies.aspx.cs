using IQSDirectory.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQSDirectory
{
    public partial class ListCompanies : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(ValidatePage() ==false)
            {
                Response.Redirect("~");
            }
            LoadData();
        }

        private bool ValidatePage()
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (url.IndexOf("/", url.Length - 1) > -1)
            {
                url = url.Remove(url.Length - 1);
                Response.Redirect(url);
            }
            string srh = url.Substring(url.ToLower().IndexOf("listcompanies"));
            object[] queryVals = srh.Split('/');
            if (queryVals.Length == 1)
            {
                Response.Redirect("ListCompanies/A/1");
            }
            else if (queryVals.Length == 3)
            {
                SrhLetter = queryVals[1].ToString();
                if (Regex.IsMatch(queryVals[2].ToString(), @"^[0-9]+$"))
                {
                    SrhPage = Convert.ToInt32(queryVals[2].ToString());
                }
                else
                {
                    SrhPage = 1;
                }
                return true;
            }
            return false;
        }

        private void LoadData()
        {
            ApiPath = wHelper.ApiUrl;
            RootPath = "../../";

            var url = string.Format("api/Clients/GetListCompanies?SrhLetter=" + SrhLetter);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            try
            {
                if(dt!= null)
                {
                    int RecPerPage = 30;
                    int Limit = RecPerPage;
                    int TotalRecs = dt.Rows.Count;
                    TotalPages = (int)Math.Ceiling((double)TotalRecs / (double)RecPerPage);
                    if (SrhPage < 1 || SrhPage > TotalPages)
                        SrhPage = 1;

                    if ((SrhPage) == TotalPages && (TotalRecs % RecPerPage) > 0)
                        Limit = ((SrhPage-1) * RecPerPage) + (TotalRecs % RecPerPage);
                    else
                        Limit = (SrhPage) * RecPerPage;

                    int Start = (SrhPage-1) * RecPerPage;

                    dt.Columns.Add("FORMATED_URL");
                    dt.Columns.Add("CITYSTATE");
                    dt.AsEnumerable().ToList().ForEach(r => {
                        r["FORMATED_URL"] = Utils.ReplaceContent(r["URL"].ToString(), 0);
                        r["CITYSTATE"] = ((r["state"].ToString() != "") & (r["city"].ToString() != "")) ? r["city"].ToString() + "," + r["state"].ToString() : r["city"].ToString().Equals(string.Empty) ? Convert.ToString(r["state"]) : Convert.ToString(r["city"]);
                    });
                    CompaniesList = dt.AsEnumerable().ToList().Skip(Start).Take(Limit).ToList();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error while creating directory page" + ex.Message);
            }
        }

        public string RootPath { get; set; }
        public string ApiPath { get; set; }
        public string SrhLetter { get; set; }
        public int SrhPage { get; set; }
        public int TotalPages { get; set; }
        public List<DataRow> CompaniesList { get; set; }
    }
}