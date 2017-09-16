using System;
using System.Text;

namespace Alexhokl.Helpers.Mp3
{
    /// <summary>
    /// This class wraps all the logic to read and write the ID3v2 (2.2, 2.3, 2.4) tag.
    /// </summary>
    public class ID3v2Tag : IMp3Tag
    {
        #region constructor
        #region public ID3v2Tag(Encoding encoding)
        public ID3v2Tag(Encoding encoding)
        {
            //this.bytes = new byte[ID3v2Tag.tagLength];
            //this.encoding = encoding;
            //byte[] buffer = this.encoding.GetBytes(ID3v2Tag.tagIdentifier);
            //for (int i = 0; i < ID3v2Tag.tagHeaderLength; i++)
            //    this.bytes[i] = buffer[i];
        }
        #endregion

        #region public ID3v2Tag(byte[] bytes, Encoding encoding)
        public ID3v2Tag(byte[] bytes, Encoding encoding)
        {
            //if (bytes.Length != ID3v1Tag.tagLength)
            //    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v1Tag, string.Format("ID3v2 tag must be of 128 bytes (supplied = {0} bytes).", bytes.Length));

            if (!encoding.GetString(bytes, 0, ID3v2Tag.tagIdentifier.Length).Equals(ID3v2Tag.tagIdentifier))
                throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Invalid tag identifier");

            this.bytes = bytes;
            this.encoding = encoding;

            /*
             * An ID3v2 tag can be detected with the following pattern:
             * $49 44 33 yy yy xx zz zz zz zz
             * Where yy is less than $FF, xx is the 'flags' byte and zz is less than
             * $80.
             */
        }
        #endregion
        #endregion

        #region properties
        #region Header
        #region MajorVersion
        public int MajorVersion
        {
            get
            {
                return (int)this.bytes[ID3v2Tag.majorVersionStartIndex];
            }
            set
            {
                this.bytes[ID3v2Tag.majorVersionStartIndex] = (byte)value;
            }
        }
        #endregion

        #region RevisionNumber
        public int RevisionNumber
        {
            get
            {
                return (int)this.bytes[ID3v2Tag.revisionNumberStartIndex];
            }
            set
            {
                this.bytes[ID3v2Tag.revisionNumberStartIndex] = (byte)value;
            }
        }
        #endregion

        #region Unsynchronisation
        public bool Unsynchronisation
        {
            get
            {
                return (this.bytes[ID3v2Tag.flagStartIndex] & 0x80) != 0;
            }
            set
            {
                if (value)
                    this.bytes[ID3v2Tag.flagStartIndex] |= 0x80;
                else
                    this.bytes[ID3v2Tag.flagStartIndex] &= 0x7f;
            }
        }
        #endregion

        #region ExtendedHeader
        public bool ExtendedHeader
        {
            get
            {
                return (this.bytes[ID3v2Tag.flagStartIndex] & 0x40) != 0;
            }
            set
            {
                if (value)
                    this.bytes[ID3v2Tag.flagStartIndex] |= 0x40;
                else
                    this.bytes[ID3v2Tag.flagStartIndex] &= 0xbf;
            }
        }
        #endregion

        #region ExperimentalIndicator
        public bool ExperimentalIndicator
        {
            get
            {
                return (this.bytes[ID3v2Tag.flagStartIndex] & 0x20) != 0;
            }
            set
            {
                if (value)
                    this.bytes[ID3v2Tag.flagStartIndex] |= 0x20;
                else
                    this.bytes[ID3v2Tag.flagStartIndex] &= 0xdf;
            }
        }
        #endregion

        #region FooterPresent
        public bool FooterPresent
        {
            get
            {
                return (this.bytes[ID3v2Tag.flagStartIndex] & 0x10) != 0;
            }
            set
            {
                if (value)
                    this.bytes[ID3v2Tag.flagStartIndex] |= 0x10;
                else
                    this.bytes[ID3v2Tag.flagStartIndex] &= 0xef;
            }
        }
        #endregion

        #region TagSize
        public int TagSize
        {
            get
            {
                int size = 0;
                for (int i = 0; i < ID3v2Tag.tagSizeLength; i++)
                {
                    int temp = (int)this.bytes[ID3v2Tag.tagSizeStartIndex + ID3v2Tag.tagSizeLength - i - 1];
                    size += temp << (i * 7);
                }
                return this.FooterPresent ? size + 20 : size + 10;
            }
        }
        #endregion
        #endregion

        #region Extended Header
        #region ExtendedHeaderSize
        public int ExtendedHeaderSize
        {
            get
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                int size = 0;
                for (int i = 0; i < ID3v2Tag.extendedHeaderSizeLength; i++)
                {
                    int temp = (int)this.bytes[ID3v2Tag.extendedHeaderSizeStartIndex + ID3v2Tag.extendedHeaderSizeLength - i - 1];
                    size += temp << (i * 7);
                }
                return size;
            }
        }
        #endregion

        #region ExtendedHeaderFlagBytesNumber
        public int ExtendedHeaderFlagBytesNumber
        {
            get
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                return (int)this.bytes[ID3v2Tag.extendedHeaderFlagBytesNumberStartIndex];
            }
        }
        #endregion

        #region CrcPresent
        public bool CrcPresent
        {
            get
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                return (this.bytes[ID3v2Tag.flagStartIndex] & 0x40) != 0;
            }
            set
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                if (value)
                    this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] |= 0x40;
                else
                    this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] &= 0xbf;
            }
        }
        #endregion

        #region TagRestrictions
        public bool TagRestrictions
        {
            get
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                return (this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] & 0x20) != 0;
            }
            set
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                if (value)
                    this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] |= 0x20;
                else
                    this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] &= 0xdf;
            }
        }
        #endregion

        #region TagUpdate
        public bool TagUpdate
        {
            get
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                return (this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] & 0x10) != 0;
            }
            set
            {
                if (!this.ExtendedHeader)
                    throw new Mp3Exception(Mp3ExceptionType.InvalidId3v2Tag, "Extended Header does not exist.");
                if (value)
                    this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] |= 0x10;
                else
                    this.bytes[ID3v2Tag.extendedHeaderFlagStartIndex] &= 0xef;
            }
        }
        #endregion


        #endregion
        #endregion

        #region IMp3Tag Members
        #region TagEncoding
        public Encoding TagEncoding
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Title
        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Artist
        public string Artist
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Album
        public string Album
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Year
        public int Year
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Comment
        public string Comment
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Track
        public int Track
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region GenreId
        public int GenreId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region TagBytes
        public byte[] TagBytes
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
        #endregion

        #region constants
        private const string tagIdentifier = "ID3";
        private const int tagHeaderLength = 10;
        //public const int tagLength = 128;
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

        private const int majorVersionStartIndex = 3;
        private const int revisionNumberStartIndex = 4;
        private const int flagStartIndex = 5;
        private const int tagSizeStartIndex = 6;
        private const int tagSizeLength = 4;
        private const int extendedHeaderSizeStartIndex = 10;
        private const int extendedHeaderSizeLength = 4;
        private const int extendedHeaderFlagBytesNumberStartIndex = 14;
        private const int extendedHeaderFlagStartIndex = 15;
        #endregion

        #region private variables
        private byte[] bytes = null;
        private Encoding encoding = Encoding.Default;
        #endregion
    }
}
