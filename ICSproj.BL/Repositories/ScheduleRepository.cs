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
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public ScheduleRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }


        public IEnumerable<ScheduleListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();
            
            return dbContext.Schedule
                .Select(e => ScheduleMapper.MapScheduleEntityToListModel(e)).ToArray();
        }

        public ScheduleDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = dbContext.Schedule.Single(t => t.Id == id);

            return ScheduleMapper.MapScheduleEntityToDetailModel(entity);
        }

        public ScheduleDetailModel InsertOrUpdate(ScheduleDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = ScheduleMapper.MapScheduleDetailModelToEntity(model);

            if (entity == null) return null;

            dbContext.Schedule.Update(entity);
            dbContext.SaveChanges();

            return ScheduleMapper.MapScheduleEntityToDetailModel(entity);
        }

        public void Delete(Guid id)
        {
            //using var dbContext = _dbContextFactory.Create();

            //var entity = new ScheduleEntity(id);

            //dbContext.Remove(entity);
            //dbContext.SaveChanges();
        }
    }
}
