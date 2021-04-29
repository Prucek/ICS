using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Mappers;
using ICSproj.BL.Models;
using ICSproj.DAL.Entities;
using ICSproj.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace ICSproj.BL.Repositories
{
    public class BandRepository : IRepository<BandDetailModel, BandListModel>
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public BandRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public BandDetailModel InsertOrUpdate(BandDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Bands.SingleOrDefault(x => x.Name == model.Name);

            if (entity == null)
            {
                entity = BandMapper.MapBandDetailModelToEntity(model);
            }

            dbContext.Bands.Update(entity);
            dbContext.SaveChanges();

            return BandMapper.MapBandEntityToDetailModel(entity);
        }

        public BandDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            BandEntity entity = dbContext.Bands.Include(x => x.Photos)
                .Include(x => x.PerformanceMapping)
                .ThenInclude(x => x.Stage)
                .Single(t => t.Id == id);

            return BandMapper.MapBandEntityToDetailModel(entity);
        }

        public BandDetailModel GetByName(string bandName)
        {
            using var dbContext = _dbContextFactory.Create();

            BandEntity entity = dbContext.Bands.Single(t => t.Name == bandName);

            return BandMapper.MapBandEntityToDetailModel(entity);
        }

        public bool Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = new BandEntity(id);

            dbContext.Remove(entity);
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public ICollection<BandListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();

            return dbContext.Bands
                .Select(e => BandMapper.MapBandEntityToListModel(e)).ToArray();
        }
    }
}
