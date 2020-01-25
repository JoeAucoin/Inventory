using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace GIBS.Modules.Inventory.Components
{
    public static class Expando
    {

        public static DataTable ToExpandoDataTable(this IEnumerable<dynamic> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;

            var dt = new DataTable();
            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                dt.Columns.Add(key);
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }

        public static DataTable ToDataTable(this IEnumerable<dynamic> items)
        {
            var data = items.ToArray();
            if (data.Count() == 0) return null;

            var dt = new DataTable();
            foreach (var key in ((IDictionary<string, object>)data[0]).Keys)
            {
                dt.Columns.Add(key);
            }
            foreach (var d in data)
            {
                dt.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());
            }
            return dt;
        }


        
    //    private string _connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString(); // from web.config, or wherever you store 
        

                public static SqlDataReader executeProcedure(string commandName, Dictionary<string, object> vparams)
                {
                    SqlConnection conn = new SqlConnection(DotNetNuke.Common.Utilities.Config.GetConnectionString());
                    conn.Open();
                    SqlCommand comm = conn.CreateCommand();
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = commandName;
                    if (vparams != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in vparams)
                            comm.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }
                    return comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }

    }
}