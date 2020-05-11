using System;
using System.Collections.Generic;
using System.Text;


namespace Alexhokl.Helpers.Data.Olap
{
    /// <summary>
    /// This class wraps the logic for building a MDX query string.
    /// </summary>
    /// <remarks>Note that, currently, this class only supports 3 axes.</remarks>
    public class OlapQuery
    {
        #region constructors
        public OlapQuery()
        {
        }
        #endregion

        #region public methods
        public void AddSet(OlapSet set, AxesType type)
        {
            switch (type)
            {
                case AxesType.Columns:
                    this.setsOnColumns.Add(set);
                    break;
                case AxesType.Rows:
                    this.setsOnRows.Add(set);
                    break;
                case AxesType.Slicer:
                    this.setsOnSlice.Add(set);
                    break;
                default:
                    throw new ApplicationException("Invalid axis type.");
            }
        }
        #endregion

        #region properties
        #region CubeName
        /// <summary>
        /// Cube name
        /// </summary>
        public string CubeName
        {
            get
            {
                return this.cubeName;
            }
            set
            {
                this.cubeName = value;
            }
        }
        #endregion

        #region QueryString
        /// <summary>
        /// MDX query string
        /// </summary>
        public string QueryString
        {
            get
            {
                string columnsStr = string.Empty;
                string rowsStr = string.Empty;
                string slicersStr = string.Empty;
                int index = 0;

                #region columns
                StringBuilder columns = new StringBuilder();
                foreach (OlapSet set in this.setsOnColumns)
                {
                    columns.Append(set.ToString());
                    columns.Append(", ");
                }
                columns.Append(" ON COLUMNS,");
                columnsStr = columns.ToString();
                index = columnsStr.LastIndexOf(", ", StringComparison.InvariantCulture);
                if (index >= 0)
                    columnsStr = columnsStr.Remove(index, 2);
                #endregion

                #region rows
                StringBuilder rows = new StringBuilder();
                foreach (OlapSet set in this.setsOnRows)
                {
                    rows.Append(set.ToString());
                    rows.Append(", ");
                }
                rows.Append(" ON ROWS");
                rowsStr = rows.ToString();
                index = rowsStr.LastIndexOf(", ", StringComparison.InvariantCulture);
                if (index >= 0)
                    rowsStr = rowsStr.Remove(index, 2);
                #endregion

                #region slicers
                StringBuilder slicers = new StringBuilder();
                slicers.Append(this.setsOnSlice.Count != 0 ? "WHERE " : string.Empty);
                foreach (OlapSet set in this.setsOnSlice)
                {
                    slicers.Append(set.ToString());
                    slicers.Append(", ");
                }
                slicersStr = slicers.ToString();
                index = slicersStr.LastIndexOf(", ", StringComparison.InvariantCulture);
                if (index >= 0)
                    slicersStr = slicersStr.Remove(index, 2);
                #endregion

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("SELECT");
                builder.AppendLine(columnsStr);
                builder.AppendLine(rowsStr);
                builder.AppendLine(string.Format("FROM [{0}]", this.cubeName));
                builder.AppendLine(slicersStr);
                return builder.ToString();
            }
        }
        #endregion
        #endregion

        #region helper methods
        #endregion

        #region private variables
        string cubeName = null;
        List<OlapSet> setsOnColumns = new List<OlapSet>();
        List<OlapSet> setsOnRows = new List<OlapSet>();
        List<OlapSet> setsOnSlice = new List<OlapSet>();
        #endregion
    }
}
