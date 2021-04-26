using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ICSproj.App.Extensions;
using ICSproj.App.Services;
using ICSproj.App.ViewModels;
using ICSproj.App.ViewModels.Interfaces;
using ICSproj.BL.Models;
using ICSproj.BL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ICSproj.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddSingleton<IRepository<BandDetailModel, BandListModel>, BandRepository>();
            services.AddSingleton<IRepository<StageDetailModel, StageListModel>, StageRepository>();
            services.AddSingleton<IRepository<PhotoDetailModel, PhotoListModel>, PhotoRepository>();
            services.AddSingleton<IRepository<ScheduleDetailModel, ScheduleListModel>, ScheduleRepository>();

            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IBandListViewModel, BandListViewModel>();
            services.AddFactory<IBandDetailViewModel, BandDetailViewModel>();
            services.AddSingleton<IStageListViewModel, StageListViewModel>();
            services.AddFactory<IStageDetailViewModel, StageDetailViewModel>();
            services.AddSingleton<IPhotoListViewModel, PhotoListViewModel>();
            services.AddFactory<IPhotoDetailViewModel, PhotoDetailViewModel>();
            services.AddSingleton<IScheduleListViewModel, ScheduleListViewModel>();
            services.AddFactory<IScheduleDetailViewModel, ScheduleDetailViewModel>();
        }

        // TODO Finish this class according to the reference project
        // TODO Discuss app configuration and DbContext used in this class
    }
}
