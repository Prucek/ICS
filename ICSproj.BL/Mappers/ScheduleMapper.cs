﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;
using ICSproj.DAL.Entities;

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
                BandDescription = entity.Band.Description,
                StageId = entity.Stage.Id,
                StageName = entity.Stage.Name,
                StageDescription = entity.Stage.Description,
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
                Band = BandMapper.MapBandEntityToDetailModel(entity.Band),
                StageId = entity.StageId,
                Stage = StageMapper.MapStageEntityToDetailModel(entity.Stage),
                PerformanceDateTime = entity.PerformanceDateTime,
                PerformanceDuration = entity.PerformanceDuration
            };
        }

        public static ScheduleEntity MapScheduleDetailModelToEntity(ScheduleDetailModel model)
        {
            return new ScheduleEntity
            {
                Id = model.Id,
                BandId = model.BandId,
                Band = BandMapper.MapBandDetailModelToEntity(model.Band),
                StageId = model.StageId,
                Stage = StageMapper.MapStageDetailModelToEntity(model.Stage),
                PerformanceDateTime = model.PerformanceDateTime,
                PerformanceDuration = model.PerformanceDuration
            };
        }
    }
}
