using System.IO;
using System.Linq;

namespace mego.Core.Models
{
    public class ImageSettings
    {
        public int MaxSize { get; set; }

        public string [] AcceptedTypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedTypes.Any(a => a == Path.GetExtension(fileName)
            .ToLower());
        }
    }
}
