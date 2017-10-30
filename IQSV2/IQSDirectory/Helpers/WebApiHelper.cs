﻿using Newtonsoft.Json;
using System.Data;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace IQSDirectory.Helpers
{
    public class WebApiHelper
    {

        public DataSet GetDataSetFromWebApi(string path)
        {
            var url = string.Format(path);
            HttpResponseMessage response = Utils.Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                DataSet ds = JsonConvert.DeserializeObject<DataSet>(res);
                return ds;
            }
            return null;
        }

        public DataTable GetDataTableFromWebApi(string path)
        {
            var url = string.Format(path);
            HttpResponseMessage response = Utils.Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                DataSet dt = JsonConvert.DeserializeObject<DataSet>(res);
                return dt.Tables[0];
            }
            return null;
        }

        public int GetExecuteNonQueryResFromWebApi(string path)
        {
            var url = string.Format(path);
            HttpResponseMessage response = Utils.Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                int ret = JsonConvert.DeserializeObject<int>(res);
                return ret;
            }
            return 0;
        }

        
        public string GetExecuteNonQueryStringResFromWebApi(string path)
        {
            var url = string.Format(path);
            HttpResponseMessage response = Utils.Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                string ret = JsonConvert.DeserializeObject<string>(res);
                return ret;
            }
            else
                return "Error";// + response.StatusCode.ToString();
        }


        public string ApiUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Api"].ToString();
            }
        }

        public string NewsDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["NewsDirectory"].ToString();
            }
        }

        public string BlogDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BlogDirectory"].ToString();
            }
        }

        public string WebUrl
        { 
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WebURL"].ToString();
            }
        }

        /* SJ added profile configs */
        public string ProfileFromEmailAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileFromEmailAddress"].ToString();
            }
        }
        public string ProfileCCEmailAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileCCEmailAddress"].ToString();
            }
        }
        public string ProfileEmailSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileEmailSubject"].ToString();
            }
        }
        public string ProfileNonExistEmailSubject
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ProfileNonExistEmailSubject"].ToString();
            }
        }


    }
}