using ICSproj.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace ICSproj.BL.Factories
{
    class DbContextFactory : IDbContextFactory
    {
        public FestivalDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FestivalDbContext>();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = TasksDB;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");
            return new FestivalDbContext(builder.Options);
        }
    }
}
