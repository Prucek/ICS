using System;
using ICSproj.Entities;

namespace ICSproj
{
    class Program
    {
        static void Main(string[] args)
        {
            ScheduleEntity Schedule = new ScheduleEntity();

            //Schedule.PerformanceDateTime = DateTime.MinValue;
            //Schedule.PerformanceDuration = TimeSpan.FromHours(2.5);
            //Schedule.checkDateTimeArtithmetics();

            /* Notes:
                - 2 foreign keys solution for photos relationship (1 filled, 1 null)
                - demonstrate datetime functionality and checking
            
               Todo:
                + Search for filesystem-path-optimized struct/object type
                - Discuss global sln type (Console App ???)
                + Push to azure tohether
            */
        }
    }
}
