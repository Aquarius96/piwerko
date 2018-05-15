using System.IO;
using System.Linq;

namespace Piwerko.Api.Helpers
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }
        public string DefaultBeerPhotoUrl { get; set; }
        public string DefaultBreweryPhotoUrl { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
        }
        
        
    }
}