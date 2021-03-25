﻿// <auto-generated />
using System;
using ICSproj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ICSproj.Migrations
{
    [DbContext(typeof(FestivalDbContext))]
    partial class FestivalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ICSproj.Entities.BandEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionLong")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginCountry")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bands");
                });

            modelBuilder.Entity("ICSproj.Entities.PhotoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BandEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SrcPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StageEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BandEntityId");

                    b.HasIndex("StageEntityId");

                    b.ToTable("PhotoEntity");
                });

            modelBuilder.Entity("ICSproj.Entities.ScheduleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PerformanceDateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("PerformanceDuration")
                        .HasColumnType("time");

                    b.Property<Guid>("StageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BandId");

                    b.HasIndex("StageId");

                    b.ToTable("Program");
                });

            modelBuilder.Entity("ICSproj.Entities.StageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("ICSproj.Entities.PhotoEntity", b =>
                {
                    b.HasOne("ICSproj.Entities.BandEntity", null)
                        .WithMany("Photos")
                        .HasForeignKey("BandEntityId");

                    b.HasOne("ICSproj.Entities.StageEntity", null)
                        .WithMany("Photos")
                        .HasForeignKey("StageEntityId");
                });

            modelBuilder.Entity("ICSproj.Entities.ScheduleEntity", b =>
                {
                    b.HasOne("ICSproj.Entities.BandEntity", "Band")
                        .WithMany("PerformanceMapping")
                        .HasForeignKey("BandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICSproj.Entities.StageEntity", "Stage")
                        .WithMany("PerformanceMapping")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Band");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("ICSproj.Entities.BandEntity", b =>
                {
                    b.Navigation("PerformanceMapping");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("ICSproj.Entities.StageEntity", b =>
                {
                    b.Navigation("PerformanceMapping");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
