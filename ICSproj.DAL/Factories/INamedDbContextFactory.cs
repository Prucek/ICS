using Microsoft.EntityFrameworkCore;

namespace ICSproj.DAL.Factories
{
    public interface INamedDbContextFactory<out TDbContext> where TDbContext : DbContext
    {
        TDbContext Create();
    }
}
