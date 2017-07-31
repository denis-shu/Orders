using mego.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mego.Models
{

    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }

        public bool IsRegistered { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<OrderFeature> Features { get; set; }

        public ICollection<Image> Images { get; set; }

        public Order()
        {
            Features = new Collection<OrderFeature>();

            Images = new Collection<Image>();
        }

    }
}
