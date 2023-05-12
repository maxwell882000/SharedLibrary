using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace SharedLibrary.Repositories.Interfaces
{
    public interface IMediaRepository<Context, M> : IGenericRepository<M>
    where Context : DbContext
    where M : Media
    {

    }
}
