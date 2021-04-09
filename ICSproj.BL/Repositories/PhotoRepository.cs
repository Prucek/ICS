using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Mappers;
using ICSproj.BL.Models;
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
            throw new NotImplementedException();
        }

        public PhotoDetailModel InsertOrUpdate(PhotoDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = PhotoMapper.MapPhotoDetailModelToEntity(model);

            if (entity == null) return null;

            dbContext.Photos.Update(entity);
            dbContext.SaveChanges();

            return PhotoMapper.MapPhotoEntityToDetailModel(entity);
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
