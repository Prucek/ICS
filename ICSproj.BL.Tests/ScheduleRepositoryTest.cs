using System;
using ICSproj.BL.Mappers;
using ICSproj.BL.Models;
using Xunit;
using ICSproj.BL.Repositories;
using ICSproj.DAL.Entities;

namespace ICSproj.BL.Tests
{
    public sealed class ScheduleRepositoryTest : IDisposable
    {
        private readonly ScheduleRepository _scheduleRepositorySUT;
        private readonly BandRepository _bandRepositorySUT;
        private readonly StageRepository _stageRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public ScheduleRepositoryTest()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(ScheduleRepositoryTest));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _scheduleRepositorySUT = new ScheduleRepository(_dbContextFactory);
            _bandRepositorySUT = new BandRepository(_dbContextFactory);
            _stageRepositorySUT = new StageRepository(_dbContextFactory);
        }

        [Fact]
        public void Create_WithNonExistingItem_DoesNotThrow()
        {
            var band = new BandDetailModel()
            {
                Name = "21 pilots"
            };
            _bandRepositorySUT.InsertOrUpdate(band);

            var stage = new StageDetailModel()
            {

                Name = "Cool stage"
            };
            _stageRepositorySUT.InsertOrUpdate(stage);

            var model = new ScheduleDetailModel()
            {
                BandName = band.Name,
                StageName = stage.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromHours(2)
            };

            var returnedModel = _scheduleRepositorySUT.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void Test_GetById()
        {
            var bandModel = new BandDetailModel()
            {
                Name = "Lorem Ipsum",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Jazz",
                OriginCountry = "UK"
            };
             _bandRepositorySUT.InsertOrUpdate(bandModel);

            var bandModel2 = new BandDetailModel()
            {
                Name = "Foo",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Jazz",
                OriginCountry = "UK"
            };
            _bandRepositorySUT.InsertOrUpdate(bandModel2);

            var stageModel = new StageDetailModel()
            {
                Name = "Stage Name",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel);

            var stageModel2 = new StageDetailModel()
            {
                Name = "Bar",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel2);

            var scheduleModel = new ScheduleDetailModel()
            {
                BandName = bandModel.Name,
                StageName = stageModel.Name,
                PerformanceDuration = TimeSpan.FromMinutes(90),
                PerformanceDateTime = DateTime.Now
            };
            var returnedModel = _scheduleRepositorySUT.InsertOrUpdate(scheduleModel);

            var scheduleModel2 = new ScheduleDetailModel()
            {
                BandName = bandModel2.Name,
                StageName = stageModel2.Name,
                PerformanceDuration = TimeSpan.FromMinutes(80),
                PerformanceDateTime = DateTime.Now
            };
            var returnedModel2 = _scheduleRepositorySUT.InsertOrUpdate(scheduleModel2);

            Assert.NotNull(returnedModel);
            var result = _scheduleRepositorySUT.GetById(returnedModel.Id);
            Assert.Equal(result.BandName,returnedModel.BandName);
            Assert.Equal(result.StageName, returnedModel.StageName);

            Assert.NotNull(returnedModel2);
            var result2 = _scheduleRepositorySUT.GetById(returnedModel2.Id);
            Assert.Equal(result2.BandName, returnedModel2.BandName);
            Assert.Equal(result2.StageName, returnedModel2.StageName);

        }

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}