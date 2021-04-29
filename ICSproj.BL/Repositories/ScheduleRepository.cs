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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ICSproj.BL.Repositories
{
    public class ScheduleRepository : IRepository<ScheduleDetailModel,ScheduleListModel>
    {
        private readonly INamedDbContextFactory<FestivalDbContext> _dbContextFactory;

        public ScheduleRepository(INamedDbContextFactory<FestivalDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        public ICollection<ScheduleListModel> GetAll()
        {
            using var dbContext = _dbContextFactory.Create();
            
            return dbContext.Schedule.Include(x => x.Band)
                .Include(x => x.Stage)
                .Select(e => ScheduleMapper.MapScheduleEntityToListModel(e)).ToArray();
        }

        public ScheduleDetailModel GetById(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            ScheduleEntity entity = dbContext.Schedule.Include(x =>x.Band)
                .Include(x => x.Stage)
                .Single(t => t.Id == id);

            return ScheduleMapper.MapScheduleEntityToDetailModel(entity);
        }

        private IEnumerable<ScheduleDetailModel> GetByStageName(string stageName)
        {
            using var dbContext = _dbContextFactory.Create();

            IEnumerable<ScheduleDetailModel> retVal;
            try
            {
                retVal = dbContext.Schedule.Include(x => x.Band)
                    .Include(x => x.Stage)
                    .Select(e => ScheduleMapper.MapScheduleEntityToDetailModel(e)).ToArray()
                    .Where(t => t.StageName == stageName);
            }
            catch (Exception e)
            {
                return null;
            }

            return retVal;
        }

        public ScheduleEntity GetByModel(ScheduleDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            ScheduleEntity retVal;
            try
            {
                retVal = dbContext.Schedule.Include(x => x.Band)
                    .Include(x => x.Stage)
                    .Single(t => t.Band.Name == model.BandName && t.Stage.Name == model.StageName && t.PerformanceDateTime == model.PerformanceDateTime);
            }
            catch (Exception e)
            {
                return null;
            }

            return retVal;
        }

        private bool CanBePerformed(ScheduleDetailModel model)
        {
            var scheduleDetailModels = GetByStageName(model.StageName);

            foreach (var t in scheduleDetailModels.Where(t => 
                    (t.PerformanceDateTime < model.PerformanceDateTime &&
                                                    t.PerformanceDateTime + t.PerformanceDuration > model.PerformanceDateTime)  
                    // model starts in another performance
                    ||
                    // model ends in another performance
                   (t.PerformanceDateTime < model.PerformanceDateTime + model.PerformanceDuration &&
                    t.PerformanceDateTime + t.PerformanceDuration > model.PerformanceDateTime + model.PerformanceDuration))) 
            {
                if (t.BandName != model.BandName) return false;

                // is collision but should update
                using var dbContext = _dbContextFactory.Create();
                DeleteByModel(model);
                return true;
            }

            return true;
        }

        public ScheduleDetailModel InsertOrUpdate(ScheduleDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            if (!CanBePerformed(model))
            {
                return null;
            }

            var entity = dbContext.Schedule
                .SingleOrDefault(x => x.Band.Name == model.BandName && x.Stage.Name == model.StageName);

            var band = dbContext.Bands.SingleOrDefault(t => t.Name == model.BandName);
            var stage = dbContext.Stages.SingleOrDefault(t => t.Name == model.StageName);

            if (band == null || stage == null)
                return null;

            entity ??= ScheduleMapper.MapScheduleDetailModelToEntity(model,
                dbContext.Bands.Single(t => t.Name == model.BandName),
                dbContext.Stages.Single(t => t.Name == model.StageName));

            band.PerformanceMapping.Add(entity);
            stage.PerformanceMapping.Add(entity);

            dbContext.Schedule.Update(entity);
            dbContext.Bands.Update(band);
            dbContext.Stages.Update(stage);
            dbContext.SaveChanges();

            return ScheduleMapper.MapScheduleEntityToDetailModel(entity);
        }

        public bool Delete(Guid id)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = new ScheduleEntity(id);

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

        public bool DeleteByModel(ScheduleDetailModel model)
        {
            using var dbContext = _dbContextFactory.Create();

            var entity = GetByModel(model);
            if (entity == null)
                return false;

            Delete(entity.Id);
            return true;

        }
    }
}
