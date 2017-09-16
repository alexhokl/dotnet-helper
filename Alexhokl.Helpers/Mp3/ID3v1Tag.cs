using System;
using System.Text;

namespace Alexhokl.Helpers.Mp3
{
    /// <summary>
    /// This class wraps all the logic to read and write the ID3v1 tag.
    /// </summary>
    public class ID3v1Tag : IMp3Tag
    {
        #region constructor
        #region public ID3v1Tag(Encoding encoding)
        public ID3v1Tag(Encoding encoding)
        {
            this.bytes = new byte[ID3v1Tag.tagLength];
            this.encoding = encoding;
            byte[] buffer = this.encoding.GetBytes(ID3v1Tag.tagHeader);
            for (int i = 0; i < ID3v1Tag.tagHeaderLength; i++)
                this.bytes[i] = buffer[i];
        }
        #endregion

        #region public ID3v1Tag(byte[] bytes, Encoding encoding)
        public ID3v1Tag(byte[] bytes, Encoding encoding)
        {
            if (bytes.Length != ID3v1Tag.tagLength)
                throw new Mp3Exception(Mp3ExceptionType.InvalidId3v1Tag, string.Format("ID3v1 tag must be of 128 bytes (supplied = {0} bytes).", bytes.Length));

            if (!encoding.GetString(bytes, 0, ID3v1Tag.tagHeaderLength).Equals(ID3v1Tag.tagHeader))
                throw new Mp3Exception(Mp3ExceptionType.InvalidId3v1Tag, "Invalid tag header");

            this.bytes = bytes;
            this.encoding = encoding;
        }
        #endregion
        #endregion

        #region properties
        #region TagEncoding
        public Encoding TagEncoding
        {
            get
            {
                return this.encoding;
            }
            set
            {
                this.encoding = value;
            }
        }
        #endregion

        #region Title
        public string Title
        {
            get
            {
                return encoding.GetString(bytes, ID3v1Tag.titleStartIndex, ID3v1Tag.titleLength).Trim();
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.Trim());
                for (int i = 0; i < ID3v1Tag.titleLength; i++)
                    this.bytes[ID3v1Tag.titleStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region Artist
        public string Artist
        {
            get
            {
                return encoding.GetString(bytes, ID3v1Tag.artistStartIndex, ID3v1Tag.artistLength).Trim();
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.Trim());
                for (int i = 0; i < ID3v1Tag.artistLength; i++)
                    this.bytes[ID3v1Tag.artistStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region Album
        public string Album
        {
            get
            {
                return encoding.GetString(bytes, ID3v1Tag.albumStartIndex, ID3v1Tag.albumLength).Trim();
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.Trim());
                for (int i = 0; i < ID3v1Tag.albumLength; i++)
                    this.bytes[ID3v1Tag.albumStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region Year
        public int Year
        {
            get
            {
                return Int32.Parse(encoding.GetString(bytes, ID3v1Tag.yearStartIndex, ID3v1Tag.yearLength).Trim());
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.ToString().Trim());
                for (int i = 0; i < ID3v1Tag.yearLength; i++)
                    this.bytes[ID3v1Tag.yearStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region Comment
        public string Comment
        {
            get
            {
                return encoding.GetString(bytes, ID3v1Tag.commentStartIndex, ID3v1Tag.commentLength).Trim();
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.Trim());
                for (int i = 0; i < ID3v1Tag.commentLength; i++)
                    this.bytes[ID3v1Tag.commentStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region Track
        public int Track
        {
            get
            {
                return Int32.Parse(encoding.GetString(bytes, ID3v1Tag.trackStartIndex, ID3v1Tag.trackLength).Trim());
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.ToString().Trim());
                for (int i = 0; i < ID3v1Tag.trackLength; i++)
                    this.bytes[ID3v1Tag.trackStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region GenreId
        public int GenreId
        {
            get
            {
                return Int32.Parse(encoding.GetString(bytes, ID3v1Tag.genreIdStartIndex, ID3v1Tag.genreIdLength).Trim());
            }
            set
            {
                byte[] buffer = this.encoding.GetBytes(value.ToString().Trim());
                for (int i = 0; i < ID3v1Tag.genreIdLength; i++)
                    this.bytes[ID3v1Tag.genreIdStartIndex + i] = buffer[i];
            }
        }
        #endregion

        #region TagBytes
        public byte[] TagBytes
        {
            get
            {
                return this.bytes;
            }
        }
        #endregion
        #endregion

        #region constants
        private const string tagHeader = "TAG";
        private const int tagHeaderLength = 3;
        public const int tagLength = 128;
        private const int titleStartIndex = 3;
        private const int titleLength = 30;
        private const int artistStartIndex = 33;
        private const int artistLength = 30;
        private const int albumStartIndex = 63;
        private const int albumLength = 30;
        private const int yearStartIndex = 93;
        private const int yearLength = 4;
        private const int commentStartIndex = 97;
        private const int commentLength = 30;
        private const int trackStartIndex = 126;
        private const int trackLength = 1;
        private const int genreIdStartIndex = 127;
        private const int genreIdLength = 1;
        #endregion

        #region private variables
        private byte[] bytes = null;
        private Encoding encoding = Encoding.Default;
        #endregion
    }
}
