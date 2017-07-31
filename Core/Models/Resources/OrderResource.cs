using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace mego.Models.Resources
{
    public class OrderResource
    {

        public int Id { get; set; }

        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }

        public OrderResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}
