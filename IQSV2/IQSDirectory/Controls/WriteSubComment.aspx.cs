using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace IQSDirectory
{

    public partial class WriteSubComment : System.Web.UI.Page
    {
        public static string rootDirPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            rootDirPath = Request.QueryString["p"].ToString();
            //CaptchaReview1.UrlPath = rootDirPath;
        }
    }
}