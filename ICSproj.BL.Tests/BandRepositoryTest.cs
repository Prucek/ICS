using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;
using ICSproj.BL.Repositories;
using Xunit;

namespace ICSproj.BL.Tests
{
    public class BandRepositoryTest : IDisposable
    {
        private readonly ScheduleRepository _scheduleRepositorySUT;
        private readonly BandRepository _bandRepositorySUT;
        private readonly StageRepository _stageRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public BandRepositoryTest()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(BandRepositoryTest));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _scheduleRepositorySUT = new ScheduleRepository(_dbContextFactory);
            _bandRepositorySUT = new BandRepository(_dbContextFactory);
            _stageRepositorySUT = new StageRepository(_dbContextFactory);
        }

        [Fact]
        public void InsertOrUpdate_Create()
        {
            var band = new BandDetailModel()
            {
                Name = "Jaromir Nohavica"
            };
            var returnedModel = _bandRepositorySUT.InsertOrUpdate(band);

            var stage = new StageDetailModel()
            {
                Name = "Kvasov"
            };
            _stageRepositorySUT.InsertOrUpdate(stage);

            var model = new ScheduleDetailModel()
            {
                BandName = band.Name,
                StageName = stage.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromHours(2)
            };
            _scheduleRepositorySUT.InsertOrUpdate(model);

            var model2 = new ScheduleDetailModel()
            {
                BandName = band.Name,
                StageName = stage.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromHours(4)
            };
            _scheduleRepositorySUT.InsertOrUpdate(model2);

            Assert.NotNull(returnedModel);
        }


        [Fact]
        public void GetById_Create()
        {
            var bandModel = new BandDetailModel()
            {
                Name = "The Roots",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Jazz",
                OriginCountry = "US"
            };
            var res1 = _bandRepositorySUT.InsertOrUpdate(bandModel);

            var bandModel2 = new BandDetailModel()
            {
                Name = "Foo Figthers",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Rock",
                OriginCountry = "US"
            };
            var res2 = _bandRepositorySUT.InsertOrUpdate(bandModel2);

            var stageModel = new StageDetailModel()
            {
                Name = "Rock Stage",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel);

            var stageModel2 = new StageDetailModel()
            {
                Name = "Pop Stage",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel2);

            var model = new ScheduleDetailModel()
            {
                BandName = bandModel.Name,
                StageName = stageModel.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromHours(2)
            };
            _scheduleRepositorySUT.InsertOrUpdate(model);

            var model2 = new ScheduleDetailModel()
            {
                BandName = bandModel2.Name,
                StageName = stageModel2.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromHours(4)
            };
            _scheduleRepositorySUT.InsertOrUpdate(model2);

            Assert.NotNull(res1);
            Assert.NotNull(res2);

            var returnedModel = _bandRepositorySUT.GetById(res1.Id);
            var returnedModel2 = _bandRepositorySUT.GetById(res2.Id);

            Assert.Equal(res1.Name, returnedModel.Name);
            Assert.Equal(res2.Name, returnedModel2.Name);

            Assert.Single(returnedModel.Schedule);
            Assert.Single(returnedModel2.Schedule);
        }

        [Fact]
        public void Delete_ShouldDelete()
        {
            var bandModel = new BandDetailModel()
            {
                Name = "Random",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Metal",
                OriginCountry = "DE"
            };
            var returnedModel = _bandRepositorySUT.InsertOrUpdate(bandModel);

            var stageModel = new StageDetailModel()
            {
                Name = "This stage",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel);

            var scheduleModel = new ScheduleDetailModel()
            {
                BandName = "Random",
                StageName = stageModel.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromMinutes(80),
            };
            _scheduleRepositorySUT.InsertOrUpdate(scheduleModel);

            _bandRepositorySUT.Delete(returnedModel.Id);

            using var dbxAssert = _dbContextFactory.Create();
            Assert.False(dbxAssert.Stages.Any(i => i.Id == returnedModel.Id));

        }

        [Fact]
        public void Delete_ShouldNotDelete()
        {
            var bandModel = new BandDetailModel()
            {
                Name = "The Wombats",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Metal",
                OriginCountry = "DE"
            };
            var returnedModel = _bandRepositorySUT.InsertOrUpdate(bandModel);

            var stageModel = new StageDetailModel()
            {
                Name = "Random",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel);

            var scheduleModel = new ScheduleDetailModel()
            {
                BandName = bandModel.Name,
                StageName = stageModel.Name,
                PerformanceDateTime = DateTime.Today,
                PerformanceDuration = TimeSpan.FromMinutes(80),
            };
            var retSched = _scheduleRepositorySUT.InsertOrUpdate(scheduleModel);

            var result = _bandRepositorySUT.Delete(retSched.StageId);
            Assert.False(result);

            using var dbxAssert = _dbContextFactory.Create();
            Assert.True(dbxAssert.Bands.Any(i => i.Id == returnedModel.Id));
        }

        [Fact]
        public void InsertOrUpdate_ShouldUpdate()
        {
            Seed();

            var bandModel = new BandDetailModel()
            {
                Name = "Foo",
                Description = "Description"
            };

            var returnedModel = _bandRepositorySUT.InsertOrUpdate(bandModel);
            Assert.NotNull(returnedModel);
            Assert.Equal(3, _bandRepositorySUT.GetAll().Count());
        }

        [Fact]
        public void InsertOrUpdate_ShouldAdd()
        {

            Seed();
            Assert.Equal(3, _scheduleRepositorySUT.GetAll().Count());
            Assert.Equal(3, _stageRepositorySUT.GetAll().Count());
            Assert.Equal(3, _bandRepositorySUT.GetAll().Count());

            var bandModel = new BandDetailModel()
            {
                Name = "Nonexistent",
                Description = "Description",
            };

            var returnedModel = _bandRepositorySUT.InsertOrUpdate(bandModel);
            Assert.NotNull(returnedModel);
            Assert.Equal(4, _bandRepositorySUT.GetAll().Count());

        }

        [Fact]
        public void GetAll()
        {
            Seed();
            var returned = _bandRepositorySUT.GetAll();
            Assert.Equal(3, returned.Count());
        }

        [Fact]
        public void GetAll_Empty()
        {
            var returned = _bandRepositorySUT.GetAll();
            Assert.Empty(returned);
        }

        private void Seed()
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
                Genre = "Rock",
                OriginCountry = "US"
            };
            _bandRepositorySUT.InsertOrUpdate(bandModel2);
            var bandModel3 = new BandDetailModel()
            {
                Name = "Svieca vo vetre",
                Description = "Description",
                DescriptionLong = "Longer description",
                Genre = "Punk-Rock",
                OriginCountry = "SK"
            };
            _bandRepositorySUT.InsertOrUpdate(bandModel3);
            // =============================================
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
            var stageModel3 = new StageDetailModel()
            {
                Name = "Pekny stage",
                Description = "Description"
            };
            _stageRepositorySUT.InsertOrUpdate(stageModel3);
            // =============================================
            var scheduleModel1 = new ScheduleDetailModel()
            {
                BandName = bandModel.Name,
                StageName = stageModel.Name,
                PerformanceDuration = TimeSpan.FromMinutes(90),
                PerformanceDateTime = DateTime.Today
            };
            _scheduleRepositorySUT.InsertOrUpdate(scheduleModel1);

            var scheduleModel2 = new ScheduleDetailModel()
            {
                BandName = bandModel2.Name,
                StageName = stageModel2.Name,
                PerformanceDuration = TimeSpan.FromMinutes(80),
                PerformanceDateTime = DateTime.Today
            };
            _scheduleRepositorySUT.InsertOrUpdate(scheduleModel2);

            var scheduleModel3 = new ScheduleDetailModel()
            {
                BandName = bandModel3.Name,
                StageName = stageModel3.Name,
                PerformanceDuration = TimeSpan.FromMinutes(120),
                PerformanceDateTime = DateTime.Today
            };
            var returnedModel3 = _scheduleRepositorySUT.InsertOrUpdate(scheduleModel3);
        }

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}
