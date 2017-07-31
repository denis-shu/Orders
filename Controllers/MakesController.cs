using AutoMapper;
using mego.Models;
using mego.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mego.Controllers
{
    public class MakesController:Controller
    {
        private readonly MegoDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(MegoDbContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;

        }


        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>>  GetMakes()
        {
            var makes = await _context.Makes.Include(i => i.Models).ToListAsync();

            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}
