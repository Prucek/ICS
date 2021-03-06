using System;
using System.Collections.Generic;
using System.Text;

namespace ICSproj.Entities
{
    public class BandEntity : BaseEntity, IBandEntity, IEquatable<BandEntity>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PhotoEntity> Photos { get; } = new List<PhotoEntity>();
        public ICollection<ScheduleEntity> PerformanceMapping { get; } = new List<ScheduleEntity>();
        public string Genre { get; set; }
        public string Origin { get; set; } // Country Code
        public string DescriptionLong { get; set; }

        /**
         * Photos & PerformanceMapping are not working in this test
         */
        public bool Equals(BandEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Description == other.Description && Genre == other.Genre && Origin == other.Origin && DescriptionLong == other.DescriptionLong;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BandEntity) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Photos, PerformanceMapping, Genre, Origin, DescriptionLong);
        }
    }
}
