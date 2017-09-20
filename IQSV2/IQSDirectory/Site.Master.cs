using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQSDirectory
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ApiPath = ConfigurationManager.AppSettings["Api"].ToString();
            }
        }
        public string ApiPath { get; set; }
    }
}