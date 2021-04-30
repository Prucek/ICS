using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Mappers;
using ICSproj.BL.Models;
using ICSproj.DAL.Factories;
using ICSproj.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSproj.BL.Repositories
{
    public class StageRepository : IRepository<StageDetailModel, StageListModel>
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public StageRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public StageDetailModel InsertOrUpdate(StageDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();
            var entity = dbContext.Stages.SingleOrDefault(x => x.Name == model.Name);

            if (entity == null)
            {
                entity = StageMapper.MapStageDetailModelToEntity(model);
            }

            dbContext.Stages.Update(entity);
            dbContext.SaveChanges();

            return StageMapper.MapStageEntityToDetailModel(entity);
        }

        public StageDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            StageEntity entity = dbContext.Stages.Include(x => x.Photos)
                .Include(x => x.PerformanceMapping)
                .ThenInclude(x => x.Band)
                .Single(t => t.Id == id);

            return StageMapper.MapStageEntityToDetailModel(entity);
        }

        public StageDetailModel GetByName(string stageName)
        {
            using var dbContext = _dbContextFactory.Create();

            StageEntity entity = dbContext.Stages.Single(t => t.Name == stageName);

            return StageMapper.MapStageEntityToDetailModel(entity);
        }

        public bool Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = new StageEntity(id);

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

        public ICollection<StageListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            return dbContext.Stages
                .Select(e => StageMapper.MapStageEntityToListModel(e)).ToArray();
        }


    }
}
