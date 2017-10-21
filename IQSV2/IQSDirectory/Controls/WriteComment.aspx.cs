using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQSDirectory
{

    public partial class WriteComment : System.Web.UI.Page
    {
        public static string rootPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            rootPath = Request.QueryString["p"].ToString();
            //CaptchaReview1.UrlPath = rootDirPath;
        }
    }
}