﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ICSproj.Entities
{
    public class ScheduleEntity : BaseEntity, IEquatable<ScheduleEntity>
    {
        public Guid BandId { get; set; }
        public BandEntity Band { get; set; }
        public Guid StageId { get; set; }
        public StageEntity Stage { get; set; }
        public DateTime PerformanceDateTime { get; set; }
        public TimeSpan PerformanceDuration { get; set; }


        //<desc>Test helper</desc>
        //public void parseDateTime()
        //{
        //    Console.WriteLine($"{this.PerformanceDateTime.Date} - date ...... {this.PerformanceDateTime.TimeOfDay} - time");
        //}

        //<desc>Test helper</desc>
        //public void checkDateTimeArtithmetics()
        //{
        //    DateTime myDate = DateTime.Now;

        //    Console.WriteLine(this.PerformanceDateTime + this.PerformanceDuration);
        //    Console.WriteLine(this.PerformanceDateTime > myDate);
        //}
        public bool Equals(ScheduleEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BandId.Equals(other.BandId) && Equals(Band, other.Band) && StageId.Equals(other.StageId) && Equals(Stage, other.Stage) && PerformanceDateTime.Equals(other.PerformanceDateTime) && PerformanceDuration.Equals(other.PerformanceDuration);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ScheduleEntity) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BandId, Band, StageId, Stage, PerformanceDateTime, PerformanceDuration);
        }
    }
}