using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.Repositories.Interfaces;
using SharedLibrary.Models;

namespace SharedLibrary.Repositories.Implementation
{
    public class GenericMediaRepository<Context, T> : GenericRepository<Context, T>, IMediaRepository<Context, T>
        where Context : DbContext
        where T : Media
    {
        public GenericMediaRepository(Context context, ILogger<GenericRepository<Context, T>> logger) : base(context, logger)
        {
        }

      

    }
}
