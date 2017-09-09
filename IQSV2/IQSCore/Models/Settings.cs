using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

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