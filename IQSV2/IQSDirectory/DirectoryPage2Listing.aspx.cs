using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
namespace IQSDirectory
{
    public partial class DirectoryPage2Listing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebApiHelper wHelper = new WebApiHelper();
            ApiPath = wHelper.ApiUrl;
            RootPath = "./";
        }

        public string ApiPath { get; set; }
        public string RootPath { get; set; }
    }
}