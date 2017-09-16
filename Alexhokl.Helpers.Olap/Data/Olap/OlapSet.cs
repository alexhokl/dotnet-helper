using System.Collections.Generic;
using System.Text;

using Microsoft.AnalysisServices.AdomdClient;


namespace Alexhokl.Helpers.Data.Olap
{
    /// <summary>
    /// This class wraps a set in a query.
    /// </summary>
    public class OlapSet
    {
        #region constructors
        public OlapSet()
        {
        }
        #endregion

        #region public methods
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.nonEmpty ? "NON EMPTY {" : "{");
            foreach (OlapTuple tuple in this.tuples)
            {
                builder.Append(tuple.ToString());
                builder.Append(", ");
            }
            builder.Append("}");
            string str = builder.ToString();
            int index = str.LastIndexOf(", ");
            if (index >= 0)
                str = str.Remove(index, 2);
            return str;
        }

        public void AddTuple(OlapTuple tuple)
        {
            this.tuples.Add(tuple);
        }

        public bool RemoveTuple(OlapTuple tuple)
        {
            return this.tuples.Remove(tuple);
        }

        public override bool Equals(object obj)
        {
            Set a = (Set)obj;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region properties
        public bool NonEmpty
        {
            get
            {
                return this.nonEmpty;
            }
            set
            {
                this.nonEmpty = value;
            }
        }
        #endregion

        #region private variables
        bool nonEmpty = false;
        List<OlapTuple> tuples = new List<OlapTuple>();
        #endregion
    }
}
