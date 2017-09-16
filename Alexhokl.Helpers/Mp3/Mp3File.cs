using System.IO;
using System.Text;

namespace Alexhokl.Helpers.Mp3
{
    /// <summary>
    /// This class wraps all the logics for reading header information of a MP3 file.
    /// </summary>
    public class Mp3File
    {
        #region constructors
        public Mp3File(string filename, Encoding encoding)
        {
            #region reads bytes from file
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream, encoding))
                {
                    FileInfo info = new FileInfo(filename);
                    fileBytes = reader.ReadBytes((int)info.Length);
                }
            }
            #endregion

            this.id3v1Tag = new ID3v1Tag(this.GetID3v1Tag(), encoding);
        }
        #endregion

        #region properties
        #region TagEncoding
        public Encoding TagEncoding
        {
            get
            {
                return this.id3v1Tag.TagEncoding;
            }
            set
            {
                this.id3v1Tag.TagEncoding = value;
            }
        }
        #endregion

        #region Title
        public string Title
        {
            get
            {
                return this.id3v1Tag.Title;
            }
        }
        #endregion

        #region Artist
        public string Artist
        {
            get
            {
                return this.id3v1Tag.Artist;
            }
        }
        #endregion

        #region Album
        public string Album
        {
            get
            {
                return this.id3v1Tag.Album;
            }
        }
        #endregion

        #region Year
        public int Year
        {
            get
            {
                return this.id3v1Tag.Year;
            }
        }
        #endregion

        #region Comment
        public string Comment
        {
            get
            {
                return this.id3v1Tag.Comment;
            }
        }
        #endregion

        #region Track
        public int Track
        {
            get
            {
                return this.id3v1Tag.Track;
            }
        }
        #endregion

        #region GenreId
        public int GenreId
        {
            get
            {
                return this.id3v1Tag.GenreId;
            }
        }
        #endregion
        #endregion

        #region helper methods
        #region private byte[] GetID3v1Tag()
        private byte[] GetID3v1Tag()
        {
            byte[] bytes = new byte[ID3v1Tag.tagLength];
            for (int i = 0; i < ID3v1Tag.tagLength; i++)
                bytes[i] = this.fileBytes[this.fileBytes.Length - ID3v1Tag.tagLength + i];
            return bytes;
        }
        #endregion
        #endregion

        #region private variables
        /// <summary>
        /// Bytes of the file
        /// </summary>
        private byte[] fileBytes = null;

        /// <summary>
        /// ID3v1 tag
        /// </summary>
        private ID3v1Tag id3v1Tag = null;
        #endregion
    }
}
