using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexhokl.Helpers.Mp3
{
    public class Mp3Exception : Exception
    {
        #region constructors
        public Mp3Exception(Mp3ExceptionType type)
        {
            this.exceptionType = type;
        }

        public Mp3Exception(Mp3ExceptionType type, string message)
            : base(message)
        {
            this.exceptionType = type;
        }
        #endregion

        #region properties
        public Mp3ExceptionType Type
        {
            get
            {
                return this.exceptionType;
            }
        }
        #endregion

        #region private variables
        private Mp3ExceptionType exceptionType;
        #endregion
    }
}
