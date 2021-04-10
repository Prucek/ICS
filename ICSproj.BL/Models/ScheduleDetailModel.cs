﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.DAL.Entities;

namespace ICSproj.BL.Models
{
    public class ScheduleDetailModel : ModelBase
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; }
        public string BandDescription { get; set; }
        public Guid StageId { get; set; }
        public string StageName { get; set; }
        public string StageDescription { get; set; }
        public DateTime PerformanceDateTime { get; set; }
        public TimeSpan PerformanceDuration { get; set; }
    }
}
