using System.Data;

using Microsoft.AnalysisServices.AdomdClient;


namespace Alexhokl.Helpers
{
    public static class OlapHelper
    {
        /// <summary>
        /// Retrieves a dataset from an OLAP databse using the specified MDX query.
        /// </summary>
        /// <param name="conn">Opened connection to the database</param>
        /// <param name="mdxQuery">MDX query</param>
        /// <returns></returns>
        public static DataSet GetData(AdomdConnection conn, string mdxQuery)
        {
            DataSet ds = new DataSet();
            AdomdCommand cmd = new AdomdCommand(mdxQuery, conn);
            AdomdDataAdapter da = new AdomdDataAdapter(cmd);
            da.Fill(ds);
            return ModifyDataType(ds, typeof(System.Decimal));
        }

        #region helper methods
        private static DataSet ModifyDataType(DataSet dataSet, System.Type dataType)
        {
            DataSet ds = new DataSet();
            foreach (DataTable dt in dataSet.Tables)
                ds.Tables.Add(ModifyDataType(dt, dataType));
            return ds;
        }

        private static DataTable ModifyDataType(DataTable dataTable, System.Type dataType)
        {
            DataTable dt = dataTable.Clone();
            foreach (DataColumn dc in dt.Columns)
                if (dc.DataType.Equals(typeof(System.Object)))
                    dc.DataType = dataType;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                    dr[dc.ColumnName] = dataRow[dc.ColumnName];
                dt.Rows.Add(dr);
            }

            return dt;
        }
        #endregion
    }
}
