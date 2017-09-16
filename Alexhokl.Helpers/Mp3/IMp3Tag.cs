using System.Text;

namespace Alexhokl.Helpers.Mp3
{
    public interface IMp3Tag
    {
        Encoding TagEncoding { get; set; }
        string Title { get; set; }
        string Artist { get; set; }
        string Album { get; set; }
        int Year { get; set; }
        string Comment { get; set; }
        int Track { get; set; }
        int GenreId { get; set; }
        byte[] TagBytes { get; }
    }
}
