using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;
using ICSproj.BL.Repositories;
using ICSproj.DAL.Entities;
using ICSproj.DAL.Factories;

namespace ICSproj.BL.Mappers
{
    public static class ScheduleMapper
    {
        public static ScheduleListModel MapScheduleEntityToListModel(ScheduleEntity entity)
        {
            return new ScheduleListModel
            {
                Id = entity.Id,
                BandId = entity.Band.Id,
                BandName = entity.Band.Name,
                StageId = entity.Stage.Id,
                StageName = entity.Stage.Name,
                PerformanceDateTime = entity.PerformanceDateTime,
                PerformanceDuration = entity.PerformanceDuration
            };
        }

        public static ScheduleDetailModel MapScheduleEntityToDetailModel(ScheduleEntity entity)
        {
            return new ScheduleDetailModel
            {
                Id = entity.Id,
                BandId = entity.BandId,
                BandName = entity.Band.Name,
                BandDescription = entity.Band.Description,
                StageId = entity.StageId,
                StageName = entity.Stage.Name,
                StageDescription = entity.Stage.Description,
                PerformanceDateTime = entity.PerformanceDateTime,
                PerformanceDuration = entity.PerformanceDuration
            };
        }

        public static ScheduleEntity MapScheduleDetailModelToEntity(ScheduleDetailModel model, BandEntity band, StageEntity stage)
        {
            return new ScheduleEntity
            {
                Id = model.Id,
                BandId = model.BandId, 
                Band = band,
                StageId = model.StageId,
                Stage = stage,
                PerformanceDateTime = model.PerformanceDateTime,
                PerformanceDuration = model.PerformanceDuration
            };
        }
    }
}
