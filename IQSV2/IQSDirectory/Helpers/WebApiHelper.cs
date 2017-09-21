using Newtonsoft.Json;
using System.Data;
using System.Net.Http;

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

        public string ApiUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["Api"].ToString();
            }
        }
    }
}