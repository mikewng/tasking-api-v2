using Microsoft.EntityFrameworkCore;
using tasking_api.Infrastructure.Context;

namespace tasking_api.Infrastructure.Data
{
    public abstract class Repository<T> where T : class
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<T> DbSet;

        protected Repository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }
    }
}