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
    public class BandRepository
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public BandRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public BandDetailModel InsertOrUpdate(BandDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = BandMapper.MapBandDetailModelToEntity(model);

            if (entity == null) return null;

            dbContext.Bands.Update(entity);
            dbContext.SaveChanges();

            return BandMapper.MapBandEntityToDetailModel(entity);
        }
        public BandDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Bands.Single(t => t.Id == id);

            return BandMapper.MapBandEntityToDetailModel(entity);
        }
    }
}
