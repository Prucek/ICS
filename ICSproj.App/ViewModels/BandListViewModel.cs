using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class BandListViewModel : IBandListViewModel
    {
        private readonly IRepository<BandDetailModel, BandListModel> _bandRepository;
        private readonly IMediator _mediator;

        public BandListViewModel(IRepository<BandDetailModel, BandListModel> bandRepository, IMediator mediator)
        {
            _bandRepository = bandRepository;
            _mediator = mediator;

            BandSelectedCommand = new RelayCommand<BandListModel>(BandSelected);
            BandNewCommand = new RelayCommand(BandNew);

            mediator.Register<UpdateMessage<BandWrapper>>(BandUpdated);
            mediator.Register<DeleteMessage<BandWrapper>>(BandDeleted);
        }

        public ObservableCollection<BandListModel> Bands { get; set; } = new ObservableCollection<BandListModel>();

        public ICommand BandSelectedCommand { get; }
        public ICommand BandNewCommand { get; }

        // click button for adding new band
        private void BandNew() => _mediator.Send(new NewMessage<BandWrapper>());

        // click button for selecting existing band
        private void BandSelected(BandListModel band) => _mediator.Send(new SelectedMessage<BandWrapper> { Id = band.Id });

        // click button for updating band
        private void BandUpdated(UpdateMessage<BandWrapper> _) => Load();

        // click button for deleting band
        private void BandDeleted(DeleteMessage<BandWrapper> _) => Load();

        // provides loading band from repository
        public void Load()
        {
            Bands.Clear();
            var bands = _bandRepository.GetAll();
            Bands.AddRange(bands);
        }
        
        /*
       public override void LoadInDesignMode()
       {
          //Use in case designer wants to put some extern data in design time
       }
       */
    }
}
