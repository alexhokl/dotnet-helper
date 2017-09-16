using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexhokl.Helpers.Web
{
    public class FilterStream : Stream
    {
        #region constructors
        public FilterStream(Stream stream, Func<string, string> filter)
        {
            this.stream = stream;
            this.filter = filter;
        }

        public FilterStream(Stream stream, Func<string, string> filter, Encoding encoding)
            : this(stream, filter)
        {
            this.encoding = encoding;
        }
        #endregion

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            string s = encoding.GetString(buffer);

            s = this.filter(s);

            byte[] outdata = encoding.GetBytes(s);
            this.stream.Write(outdata, 0, outdata.GetLength(0));
        }

        #region private variables
        private Stream stream = null;
        private Func<string, string> filter = null;
        private Encoding encoding = Encoding.Default;
        #endregion
    }
}
