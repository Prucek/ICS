using System;
using System.Collections.Generic;
using System.Text;
using ICSproj.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICSproj
{
    public class FestivalDbContext : DbContext
    {
        public FestivalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BandEntity> Bands { get; set; }
        public DbSet<StageEntity> Stages { get; set; }
        public DbSet<ScheduleEntity> Program { get; set; }
    }
}
