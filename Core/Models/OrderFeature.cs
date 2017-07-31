using System.ComponentModel.DataAnnotations.Schema;

namespace mego.Models
{
    [Table("VehicleFeatures")]
    public class OrderFeature
    {
        public int OrderId { get; set; }

        public int FeatureId { get; set; }

        public Order Order { get; set; }

        public Feature Feature { get; set; }
    }
}
