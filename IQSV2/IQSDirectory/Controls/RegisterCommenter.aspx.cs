using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace IQSDirectory
{
    public partial class RegisterCommenter : System.Web.UI.Page
    {
        public static string RootPath = "";
        public string UserIP;
        protected void Page_Load(object sender, EventArgs e)
        {
            RootPath = "../"; //Request.QueryString["p"].ToString();
            UserIP = HttpContext.Current.Request.UserHostAddress.ToString();

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Write("Register Button Clicked");
        }
    }
}