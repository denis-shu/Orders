using System.ComponentModel.DataAnnotations;

namespace mego.Core.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public int OrderId { get; set; }
    }
}
