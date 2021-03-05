using Microsoft.EntityFrameworkCore;

namespace ICSproj.Tests
{
    public class DbContextInMemoryFacotry
    {
        private readonly string _dbName;

        public DbContextInMemoryFacotry(string dbName)
        {
            _dbName = dbName;
        }
        public FestivalDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(_dbName);
            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}