using System;
using System.Collections.Generic;
using System.Text;

namespace ICSproj.Entities
{
    public class BandEntity : BaseEntity, IBandEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PhotoEntity> Photos { get; } = new List<PhotoEntity>();
        public ICollection<ScheduleEntity> PerformanceMapping { get; } = new List<ScheduleEntity>();
        public string Genre { get; set; }
        public string Origin { get; set; }
        public string DescriptionLong { get; set; }
    }
}
