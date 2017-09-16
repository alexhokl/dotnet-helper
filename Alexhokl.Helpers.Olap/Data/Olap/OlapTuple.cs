using System.Text;

using Microsoft.AnalysisServices.AdomdClient;


namespace Alexhokl.Helpers.Data.Olap
{
    /// <summary>
    /// This class wraps a tuple in a set.
    /// </summary>
    public class OlapTuple
    {
        #region constructors
        public OlapTuple(string uniqueName)
        {
            this.uniqueName = uniqueName;
        }

        public OlapTuple(string uniqueName, FunctionType functionType)
        {
            this.uniqueName = uniqueName;
            this.functionType = functionType;
        }
        #endregion

        #region public methods
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("(");
            builder.Append(this.uniqueName);
            //builder.Append("." + this.functionType.ToString());
            builder.Append(")");
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            Tuple a = obj as Tuple;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region properties
        public string UniqueName
        {
            get
            {
                return this.uniqueName;
            }
            set
            {
                this.uniqueName = value;
            }
        }
        #endregion

        #region private variables
        private string uniqueName = string.Empty;
        private FunctionType functionType = FunctionType.Members;
        #endregion
    }
}
