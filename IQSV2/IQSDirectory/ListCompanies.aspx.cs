using IQSDirectory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!IsPostBack)
            {
                LoadData();
            }
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
                SrhPage = queryVals[2].ToString();
                return true;
            }
            return false;
        }

        private void LoadData()
        {
            ApiPath = wHelper.ApiUrl;
            RootPath = "../../";
        }

        public string RootPath { get; set; }
        public string ApiPath { get; set; }
        public string SrhLetter { get; set; }
        public string SrhPage { get; set; }
    }
}