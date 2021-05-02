using ICSproj.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace ICSproj.App.ViewModels
{
    internal class DbContextInMemoryFactory : INamedDbContextFactory<FestivalDbContext>
    {
        private string designTimeConnectionString;
        public DbContextInMemoryFactory(string designTimeConnectionString)
        {
            this.designTimeConnectionString = designTimeConnectionString;
        }

       

        public FestivalDbContext Create()
        {
            DbContextOptionsBuilder<FestivalDbContext>? contextOptionsBuilder = new DbContextOptionsBuilder<FestivalDbContext>();
            return new FestivalDbContext(contextOptionsBuilder.Options);
        }
    }
}