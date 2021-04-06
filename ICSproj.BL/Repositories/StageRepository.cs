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
    public class StageRepository
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public StageRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public StageDetailModel InsertOrUpdate(StageDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();
            
            var entity = StageMapper.MapStageDetailModelToEntity(model);

            if (entity == null) return null;

            dbContext.Stages.Update(entity);
            dbContext.SaveChanges();

            return StageMapper.MapStageEntityToDetailModel(entity);
        }
        public StageDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Stages.Single(t => t.Id == id);

            return StageMapper.MapStageEntityToDetailModel(entity);
        }
    }
}
