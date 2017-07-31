using System.Threading.Tasks;

namespace mego.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
