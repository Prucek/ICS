using System;
using System.Collections.Generic;
using System.Text;

namespace ICSproj.Entities
{
    public class ScheduleEntity : BaseEntity
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
    }
}
