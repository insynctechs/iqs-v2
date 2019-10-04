using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.Collections;

namespace IQSCore.Helpers
{
    public static class ApiUtils
    {
        public static List<Dictionary<string, object>> ToDictionaryList(this DataTable table)
        {
            if (table == null)
                return null;
            return table.Rows.Cast<DataRow>()
                .Select(r => table.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c => r[c]))
                .ToList();
        }

        public static Dictionary<string, List<Dictionary<string, object>>> ToDictionary(this DataSet set)
        {
            if (set == null)
                return null;
            return set.Tables.Cast<DataTable>()
                .ToDictionary(t => t.TableName, t => t.ToDictionaryList());
        }



        public static ArrayList ConvertDataSetToJsonString(this DataSet ds)
        {
             
           
            ArrayList root = new ArrayList();
            List<Dictionary<string, object>> table;
            Dictionary<string, object> data;
            if (ds.Tables.Count == 0)
                root = null;
            else if (ds.Tables.Count == 1)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    data = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        data.Add(col.ColumnName, dr[col]);
                    }
                    root.Add(data);
                }
            }
            
            else
            {
                foreach (DataTable dt in ds.Tables)
                {
                    table = new List<Dictionary<string, object>>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        data = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            data.Add(col.ColumnName, dr[col]);
                        }
                        table.Add(data);
                    }
                    root.Add(table);
                }
            }
            return root;
        }
    }

}
