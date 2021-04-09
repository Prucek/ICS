using System;
using System.Collections.Generic;
using System.Linq;
using ICSproj.BL.Mappers;
using ICSproj.BL.Models;
using Xunit;
using ICSproj.BL.Repositories;
using ICSproj.DAL.Entities;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Reflection;

namespace ICSproj.BL.Tests
{
    public class PhotoRepositoryTest : IDisposable
    {
        private readonly ScheduleRepository _scheduleRepositorySUT;
        private readonly BandRepository _bandRepositorySUT;
        private readonly StageRepository _stageRepositorySUT;
        private readonly PhotoRepository _photoRepositorySUT;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public PhotoRepositoryTest()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(StageRepositoryTest));
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureCreated();

            _scheduleRepositorySUT = new ScheduleRepository(_dbContextFactory);
            _bandRepositorySUT = new BandRepository(_dbContextFactory);
            _stageRepositorySUT = new StageRepository(_dbContextFactory);
            _photoRepositorySUT = new PhotoRepository(_dbContextFactory);
        }

        [Fact]
        public void GetAll_NothingInDatabase()
        {
            Assert.Empty(_photoRepositorySUT.GetAll());
        }

        [Fact]
        public void InsertOrUpdate_ShouldInsert()
        {

            Image img = Image.FromFile(@"../../../Photos/33583_uu-1581261011.jpg");
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            var model = new PhotoDetailModel()
            {
                Photo = arr
            };
            var returnedModel = _photoRepositorySUT.InsertOrUpdate(model);
            Assert.NotNull(returnedModel);

            using var dbxAssert = _dbContextFactory.Create();
            Assert.True(dbxAssert.Photos.Any(i => i.Id == returnedModel.Id));

        }

        [Fact]
        public void InsertOrUpdate_ShouldNotInsert()
        {

            Image img = Image.FromFile(@"../../../Photos/33583_uu-1581261011.jpg");
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            var model = new PhotoDetailModel()
            {
                Photo = arr
            };
            var returnedModel = _photoRepositorySUT.InsertOrUpdate(model);
            Assert.NotNull(returnedModel);

            var theSameModel = new PhotoDetailModel()
            {
                Photo = arr
            };


            bool isEqual = Enumerable.SequenceEqual(returnedModel.Photo, theSameModel.Photo);
            Assert.True(isEqual);

        }

        public void Dispose()
        {
            using var dbx = _dbContextFactory.Create();
            dbx.Database.EnsureDeleted();
        }
    }
}
