using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IQSCore.Models
{
    public static class Settings
    {
        public static string Constr
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConString"].ToString();
            }
        }
    }
}