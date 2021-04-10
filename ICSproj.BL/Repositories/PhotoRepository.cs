using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Mappers;
using ICSproj.BL.Models;
using ICSproj.DAL.Entities;
using ICSproj.DAL.Factories;

namespace ICSproj.BL.Repositories
{
    public class PhotoRepository : IRepository<PhotoDetailModel, PhotoListModel>
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public PhotoRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public IEnumerable<PhotoListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            return dbContext.Photos.Select(x => PhotoMapper.MapPhotoEntityToListModel(x)).ToArray();
        }

        public PhotoDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            PhotoEntity entity = dbContext.Photos.Single(t => t.Id == id);

            return PhotoMapper.MapPhotoEntityToDetailModel(entity);
        }

        public PhotoDetailModel InsertOrUpdate(PhotoDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Photos.SingleOrDefault(x => x.Photo == model.Photo);

            if (entity == null)
            {
                entity = PhotoMapper.MapPhotoDetailModelToEntity(model);
            }

            dbContext.Photos.Update(entity);
            dbContext.SaveChanges();

            return PhotoMapper.MapPhotoEntityToDetailModel(entity);
        }

        public bool Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = new PhotoEntity(id);

            dbContext.Remove(entity);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
