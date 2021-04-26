using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ICSproj.App.Commands;
using ICSproj.App.Extensions;
using ICSproj.App.Messages;
using ICSproj.App.Services;
using ICSproj.App.ViewModels.Interfaces;
using ICSproj.App.Wrappers;
using ICSproj.BL.Models;
using ICSproj.BL.Repositories;

namespace ICSproj.App.ViewModels
{
    // We should discuss whether we want ListViewModel for Photos...maybe only DetailViewModel is sufficient
    // But it depends on designer :)
    public class PhotoListViewModel : IPhotoListViewModel
    {
        private readonly IRepository<PhotoDetailModel, PhotoListModel> _photoRepository;
        private readonly IMediator _mediator;

        public PhotoListViewModel(IRepository<PhotoDetailModel, PhotoListModel> photoRepository, IMediator mediator)
        {
            _photoRepository = photoRepository;
            _mediator = mediator;

            PhotoSelectedCommand = new RelayCommand<PhotoListModel>(PhotoSelected);
            PhotoNewCommand = new RelayCommand(PhotoNew);

            mediator.Register<UpdateMessage<PhotoWrapper>>(PhotoUpdated);
            mediator.Register<DeleteMessage<PhotoWrapper>>(PhotoDeleted);
        }

        public ObservableCollection<PhotoListModel> Photos { get; set; } = new ObservableCollection<PhotoListModel>();

        public ICommand PhotoSelectedCommand { get; }
        public ICommand PhotoNewCommand { get; }

        // click button for adding new photo
        private void PhotoNew() => _mediator.Send(new NewMessage<PhotoWrapper>());

        // click button for selecting existing photo
        private void PhotoSelected(PhotoListModel photo) => _mediator.Send(new SelectedMessage<PhotoWrapper> { Id = photo.Id });

        // click button for updating photo
        private void PhotoUpdated(UpdateMessage<PhotoWrapper> _) => Load();

        // click button for deleting photo
        private void PhotoDeleted(DeleteMessage<PhotoWrapper> _) => Load();

        // provides loading photo from repository
        public void Load()
        {
            Photos.Clear();
            var photos = _photoRepository.GetAll();
            Photos.AddRange(photos);
        }

        /*
       public override void LoadInDesignMode()
       {
          //Use in case designer wants to put some extern data in design time
       }
       */
    }
}