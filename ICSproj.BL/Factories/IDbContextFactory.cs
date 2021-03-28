using System;
using ICSproj.DAL.Factories;

namespace ICSproj.BL.Factories
{
    public interface IDbContextFactory
    {
        FestivalDbContext CreateDbContext(string[] args);
    }
}
