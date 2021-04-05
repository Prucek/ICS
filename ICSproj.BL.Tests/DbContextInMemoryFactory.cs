using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace ICSproj.BL.Tests
{
    public class DbContextInMemoryFactory : INamedDbContextFactory<FestivalDbContext>
    {
        private readonly string _dbName;

        public DbContextInMemoryFactory(string dbName)
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
