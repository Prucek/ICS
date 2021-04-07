using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Repositories;
using ICSproj.DAL.Entities;

namespace ICSproj.BL.Tests
{
    public class StageRepositoryTest : IDisposable
    {
        private readonly ScheduleRepository _scheduleRepositorySUT;
        private readonly BandRepository _bandRepositorySUT;
        private readonly StageRepository _stageRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public StageRepositoryTest()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(StageRepositoryTest));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _scheduleRepositorySUT = new ScheduleRepository(_dbContextFactory);
            _bandRepositorySUT = new BandRepository(_dbContextFactory);
            _stageRepositorySUT = new StageRepository(_dbContextFactory);
        }

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}
