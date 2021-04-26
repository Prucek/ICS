using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ICSproj.App.Commands;
using ICSproj.App.Factories;
using ICSproj.App.Messages;
using ICSproj.App.Services;
using ICSproj.App.ViewModels;
using ICSproj.App.ViewModels.Interfaces;
using ICSproj.App.Wrappers;

namespace ICSproj.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFactory<IBandDetailViewModel> _bandDetailViewModelFactory;
        private readonly IFactory<IStageDetailViewModel> _stageDetailViewModelFactory;
        private readonly IFactory<IPhotoDetailViewModel> _photoDetailViewModelFactory;
        private readonly IFactory<IScheduleDetailViewModel> _scheduleDetailViewModelFactory;

        public MainViewModel(IFactory<IBandDetailViewModel> bandDetailViewModelFactory, IFactory<IStageDetailViewModel> stageDetailViewModelFactory,
            IFactory<IPhotoDetailViewModel> photoDetailViewModelFactory, IFactory<IScheduleDetailViewModel> scheduleDetailViewModelFactory,
            IMediator mediator, IBandListViewModel bandListViewModel, IStageListViewModel stageListViewModel,
            IPhotoListViewModel photoListViewModel, IScheduleListViewModel scheduleListViewModel)
        {
            _bandDetailViewModelFactory = bandDetailViewModelFactory;
            _stageDetailViewModelFactory = stageDetailViewModelFactory;
            _photoDetailViewModelFactory = photoDetailViewModelFactory;
            _scheduleDetailViewModelFactory = scheduleDetailViewModelFactory;

            BandListViewModel = bandListViewModel;
            BandDetailViewModel = _bandDetailViewModelFactory.Create();
            StageListViewModel = stageListViewModel;
            StageDetailViewModel = _stageDetailViewModelFactory.Create();
            PhotoListViewModel = photoListViewModel;
            PhotoDetailViewModel = _photoDetailViewModelFactory.Create();
            ScheduleListViewModel = scheduleListViewModel;
            ScheduleDetailViewModel = _scheduleDetailViewModelFactory.Create();

            CloseBandDetailTabCommand = new RelayCommand<IBandDetailViewModel>(OnCloseBandDetailTabExecute);
            CloseStageDetailTabCommand = new RelayCommand<IStageDetailViewModel>(OnCloseStageDetailTabExecute);
            ClosePhotoDetailTabCommand = new RelayCommand<IPhotoDetailViewModel>(OnClosePhotoDetailTabExecute);
            CloseScheduleDetailTabCommand = new RelayCommand<IScheduleDetailViewModel>(OnCloseScheduleDetailTabExecute);

            mediator.Register<NewMessage<BandWrapper>>(OnBandNewMessage);
            mediator.Register<NewMessage<StageWrapper>>(OnStageNewMessage);
            mediator.Register<NewMessage<PhotoWrapper>>(OnPhotoNewMessage);
            mediator.Register<NewMessage<ScheduleWrapper>>(OnScheduleNewMessage);

            mediator.Register<SelectedMessage<BandWrapper>>(OnBandSelected);
            mediator.Register<SelectedMessage<StageWrapper>>(OnStageSelected);
            mediator.Register<SelectedMessage<PhotoWrapper>>(OnPhotoSelected);
            mediator.Register<SelectedMessage<ScheduleWrapper>>(OnScheduleSelected);

            mediator.Register<DeleteMessage<BandWrapper>>(OnBandDeleted);
            mediator.Register<DeleteMessage<StageWrapper>>(OnStageDeleted);
            mediator.Register<DeleteMessage<PhotoWrapper>>(OnPhotoDeleted);
            mediator.Register<DeleteMessage<ScheduleWrapper>>(OnScheduleDeleted);
        }

        // stores list and detail View Models of each entity
        public IBandListViewModel BandListViewModel { get; }

        public IBandDetailViewModel BandDetailViewModel { get; }

        public IStageListViewModel StageListViewModel { get; }

        public IStageDetailViewModel StageDetailViewModel { get; }

        public IPhotoListViewModel PhotoListViewModel { get; }

        public IPhotoDetailViewModel PhotoDetailViewModel { get; }

        public IScheduleListViewModel ScheduleListViewModel { get; }

        public IScheduleDetailViewModel ScheduleDetailViewModel { get; }

        public ObservableCollection<IBandDetailViewModel> BandDetailViewModels { get; } = new ObservableCollection<IBandDetailViewModel>();
        public ObservableCollection<IStageDetailViewModel> StageDetailViewModels { get; } = new ObservableCollection<IStageDetailViewModel>();
        public ObservableCollection<IPhotoDetailViewModel> PhotoDetailViewModels { get; } = new ObservableCollection<IPhotoDetailViewModel>();
        public ObservableCollection<IScheduleDetailViewModel> ScheduleDetailViewModels { get; } = new ObservableCollection<IScheduleDetailViewModel>();

        // store actual selected View Model
        public IBandDetailViewModel SelectedBandDetailViewModel { get; set; }
        public IStageDetailViewModel SelectedStageDetailViewModel { get; set; }
        public IPhotoDetailViewModel SelectedPhotoDetailViewModel { get; set; }
        public IScheduleDetailViewModel SelectedScheduleDetailViewModel { get; set; }

        // store commands for closing detail View Models
        public ICommand CloseBandDetailTabCommand { get; }
        public ICommand CloseStageDetailTabCommand { get; }
        public ICommand ClosePhotoDetailTabCommand { get; }
        public ICommand CloseScheduleDetailTabCommand { get; }

        // specifies action when user click on adding new band
        private void OnBandNewMessage(NewMessage<BandWrapper> _)
        {
            SelectBand(Guid.Empty);
        }

        // specifies action when user click on adding new stage
        private void OnStageNewMessage(NewMessage<StageWrapper> _)
        {
            SelectStage(Guid.Empty);
        }

        // specifies action when user click on adding new photo
        private void OnPhotoNewMessage(NewMessage<PhotoWrapper> _)
        {
            SelectPhoto(Guid.Empty);
        }

        // specifies action when user click on adding new schedule
        private void OnScheduleNewMessage(NewMessage<ScheduleWrapper> _)
        {
            SelectSchedule(Guid.Empty);
        }

        // specifies action when user click on specific band
        private void OnBandSelected(SelectedMessage<BandWrapper> message)
        {
            SelectBand(message.Id);
        }

        // specifies action when user click on specific stage
        private void OnStageSelected(SelectedMessage<StageWrapper> message)
        {
            SelectStage(message.Id);
        }

        // specifies action when user click on specific photo
        private void OnPhotoSelected(SelectedMessage<PhotoWrapper> message)
        {
            SelectPhoto(message.Id);
        }

        // specifies action when user click on specific schedule
        private void OnScheduleSelected(SelectedMessage<ScheduleWrapper> message)
        {
            SelectSchedule(message.Id);
        }

        // provides deleting of selected band
        private void OnBandDeleted(DeleteMessage<BandWrapper> message)
        {
            var band = BandDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (band != null)
            {
                BandDetailViewModels.Remove(band);
            }
        }

        // provides deleting of selected stage
        private void OnStageDeleted(DeleteMessage<StageWrapper> message)
        {
            var stage = StageDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (stage != null)
            {
                StageDetailViewModels.Remove(stage);
            }
        }

        // provides deleting of selected photo
        private void OnPhotoDeleted(DeleteMessage<PhotoWrapper> message)
        {
            var photo = PhotoDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (photo != null)
            {
                PhotoDetailViewModels.Remove(photo);
            }
        }

        // provides deleting of selected schedule
        private void OnScheduleDeleted(DeleteMessage<ScheduleWrapper> message)
        {
            var schedule = ScheduleDetailViewModels.SingleOrDefault(i => i.Model.Id == message.Id);
            if (schedule != null)
            {
                ScheduleDetailViewModels.Remove(schedule);
            }
        }
        // Select View Model of band, if not exists then create one
        private void SelectBand(Guid id)
        {
            var bandDetailViewModel =
                BandDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (bandDetailViewModel == null)
            {
                bandDetailViewModel = _bandDetailViewModelFactory.Create();
                BandDetailViewModels.Add(bandDetailViewModel);
                bandDetailViewModel.Load(id);
            }

            SelectedBandDetailViewModel = bandDetailViewModel;
        }

        // Select View Model of stage, if not exists then create one
        private void SelectStage(Guid id)
        {
            var stageDetailViewModel =
                StageDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (stageDetailViewModel == null)
            {
                stageDetailViewModel = _stageDetailViewModelFactory.Create();
                StageDetailViewModels.Add(stageDetailViewModel);
                stageDetailViewModel.Load(id);
            }

            SelectedStageDetailViewModel = stageDetailViewModel;
        }

        // Select View Model of photo, if not exists then create one
        private void SelectPhoto(Guid id)
        {
            var photoDetailViewModel =
                PhotoDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (photoDetailViewModel == null)
            {
                photoDetailViewModel = _photoDetailViewModelFactory.Create();
                PhotoDetailViewModels.Add(photoDetailViewModel);
                photoDetailViewModel.Load(id);
            }

            SelectedPhotoDetailViewModel = photoDetailViewModel;
        }

        // Select View Model of schedule, if not exists then create one
        private void SelectSchedule(Guid id)
        {
            var scheduleDetailViewModel =
                ScheduleDetailViewModels.SingleOrDefault(vm => vm.Model.Id == id);
            if (scheduleDetailViewModel == null)
            {
                scheduleDetailViewModel = _scheduleDetailViewModelFactory.Create();
                ScheduleDetailViewModels.Add(scheduleDetailViewModel);
                scheduleDetailViewModel.Load(id);
            }

            SelectedScheduleDetailViewModel = scheduleDetailViewModel;
        }

        // closes band View Model
        private void OnCloseBandDetailTabExecute(IBandDetailViewModel bandDetailViewModel)
        {
            // TODO: Check if the Detail has changes and ask user to cancel
            BandDetailViewModels.Remove(bandDetailViewModel);
        }

        // closes stage View Model
        private void OnCloseStageDetailTabExecute(IStageDetailViewModel stageDetailViewModel)
        {
            // TODO: Check if the Detail has changes and ask user to cancel
            StageDetailViewModels.Remove(stageDetailViewModel);
        }

        // closes photo View Model
        private void OnClosePhotoDetailTabExecute(IPhotoDetailViewModel photoDetailViewModel)
        {
            // TODO: Check if the Detail has changes and ask user to cancel
            PhotoDetailViewModels.Remove(photoDetailViewModel);
        }

        // closes schedule View Model
        private void OnCloseScheduleDetailTabExecute(IScheduleDetailViewModel scheduleDetailViewModel)
        {
            // TODO: Check if the Detail has changes and ask user to cancel
            ScheduleDetailViewModels.Remove(scheduleDetailViewModel);
        }
    }
}
