using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mego.Core;
using mego.Core.Models;
using mego.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace mego.Persistence
{
    public class ImageRepo : IImageRepo
    {
        private readonly MegoDbContext _context;

        public ImageRepo(MegoDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<Image>> GetImages(int orderId)
        {
            return await _context.Images
            .Where(i => i.OrderId == orderId).ToListAsync();
        }
    }
}
