using System;
using ICSproj.BL.Models;
using Xunit;
using ICSproj.BL.Repositories;

namespace ICSproj.BL.Tests
{
    public sealed class ScheduleRepositoryTest : IDisposable
    {
        private readonly ScheduleRepository _scheduleRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public ScheduleRepositoryTest()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(ScheduleRepositoryTest));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _scheduleRepositorySUT = new ScheduleRepository(_dbContextFactory);
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var band = new BandDetailModel()
            {
                Name = "21 pilots"
            };
            var stage = new StageDetailModel()
            {
                Name = "Cool stage"
            };

            var model = new ScheduleDetailModel()
            {
                Band = band,
                Stage = stage,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromHours(2)
            };

            var returnedModel = _scheduleRepositorySUT.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }



        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}
