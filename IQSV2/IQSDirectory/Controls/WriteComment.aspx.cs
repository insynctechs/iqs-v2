﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQSDirectory
{

    public partial class WriteComment : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            RootPath = Request.QueryString["p"].ToString();
            UserIP = HttpContext.Current.Request.UserHostAddress.ToString();
            //Request.QueryString["p"].ToString();
            //CaptchaReview1.UrlPath = rootDirPath;
        }

        public string RootPath { get; set; }
        public string UserIP { get; set; }
    }
}