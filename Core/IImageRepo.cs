using mego.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mego.Core
{
    public interface IImageRepo
    {
        Task<IEnumerable<Image>> GetImages(int orderId);
    }
}
