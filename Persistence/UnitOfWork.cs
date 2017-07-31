using mego.Core;
using mego.Models;
using System.Threading.Tasks;

namespace mego.Persistence
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MegoDbContext _context;

        public UnitOfWork(MegoDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
