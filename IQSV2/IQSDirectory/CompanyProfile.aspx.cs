using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;

namespace IQSDirectory
{
    public partial class CompanyProfile : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            WebApiHelper wHelper = new WebApiHelper();
            if (!IsPostBack)
            {
                DisplayClientInfo();
            }
        }

        private void DisplayClientInfo()
        {
            RootPath = "../../";
            ApiPath = wHelper.ApiUrl;
        }

        public string RootPath { get; set; }
        public string DisplayName { get; set; }
        public string CategorySK { get; set; }
        public string ApiPath { get; set; }
    }
}