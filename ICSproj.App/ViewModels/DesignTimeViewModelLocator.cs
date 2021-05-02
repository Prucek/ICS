using ICSproj.App.Services;
using ICSproj.BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSproj.App.ViewModels
{
    public class DesignTimeViewModelLocator
    {
        private const string DesignTimeConnectionString = @"inMemory:CookBook";

        public BandListViewModel BandListViewModel { get; }
        public StageListViewModel StageListViewModel { get; }
        public ScheduleListViewModel ScheduleListViewModel { get;}
        public PhotoListViewModel PhotoListViewModel { get; }
        public BandDetailViewModel BandDetailViewModel { get; set; }
        public StageDetailViewModel StageDetailViewModel { get; set; }
        public ScheduleDetailViewModel ScheduleDetailViewModel { get; set; }
        public PhotoDetailViewModel PhotoDetailViewModel { get; set; }


        public DesignTimeViewModelLocator()
        {
            var bandRepository = new BandRepository(new DbContextInMemoryFactory(DesignTimeConnectionString));
            var stageRepository = new StageRepository(new DbContextInMemoryFactory(DesignTimeConnectionString));
            var scheduleRepository = new ScheduleRepository(new DbContextInMemoryFactory(DesignTimeConnectionString));
            var photoRepository = new PhotoRepository(new DbContextInMemoryFactory(DesignTimeConnectionString));
            var mediator = new Mediator();
            var messageDialogService = new MessageDialogService();

            BandListViewModel = new BandListViewModel(bandRepository, mediator);
            StageListViewModel = new StageListViewModel(stageRepository, mediator);
            ScheduleListViewModel = new ScheduleListViewModel(scheduleRepository, mediator);
            PhotoListViewModel = new PhotoListViewModel(photoRepository, mediator);
            BandDetailViewModel = new BandDetailViewModel(bandRepository, mediator);
            StageDetailViewModel = new StageDetailViewModel(stageRepository, mediator);
            ScheduleDetailViewModel = new ScheduleDetailViewModel(scheduleRepository, mediator);
            PhotoDetailViewModel = new PhotoDetailViewModel(photoRepository, mediator);
        }
    }
}
