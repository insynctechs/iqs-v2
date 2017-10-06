using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IQSDirectory.Helpers;
using System.Data;

namespace IQSDirectory
{
    public partial class DirectorySearch : System.Web.UI.Page
    {
        WebApiHelper wHelper = new WebApiHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] == null)
                Response.Redirect("~");
            if (Request.QueryString["q"].ToString().Trim() == "")
                Response.Redirect("~");
            CheckStateSearch();
        }

        private void CheckStateSearch()
        {
            if (Request.QueryString["st"] == null)
                return;
            if (Request.QueryString["st"].ToString().Trim() == "")
                return;
            string state = Uri.UnescapeDataString(Request.QueryString["st"].ToString().Trim());
            string category = Uri.UnescapeDataString(Request.QueryString["q"].ToString().Trim());
            var url = string.Format("api/StateSearch/GetStateSearchResults?Category=" + category + "&State=" + state);
            DataTable dt = wHelper.GetDataTableFromWebApi(url);
            if (dt != null)
            {
                if ((dt.Rows[0][0].ToString() == "") || (dt.Rows[0][1].ToString() == ""))
                {
                    return;
                }
                else
                {
                    string navUrl = dt.Rows[0][0].ToString() + "/" + dt.Rows[0][1].ToString().Replace(" ", "-");
                    Response.Redirect(navUrl);
                }
            }
            else
            {
                return;
            }
        }
    }
}