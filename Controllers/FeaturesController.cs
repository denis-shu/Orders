using AutoMapper;
using mego.Models;
using mego.Models.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mego.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MegoDbContext _context;

        public FeaturesController(MegoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        [HttpGet("/api/features")]
        [Authorize]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();

            return _mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);

        }
    }
}
