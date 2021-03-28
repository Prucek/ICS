using System;
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
        public BandDetailModel Band { get; set; }
        public Guid StageId { get; set; }
        public StageDetailModel Stage { get; set; }
        public DateTime PerformanceDateTime { get; set; }
        public TimeSpan PerformanceDuration { get; set; }
    }
}
