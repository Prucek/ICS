using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICSproj.App.Commands;
using ICSproj.App.Messages;
using ICSproj.App.Services;
using ICSproj.App.ViewModels.Interfaces;
using ICSproj.App.Wrappers;
using ICSproj.BL.Models;
using ICSproj.BL.Repositories;

namespace ICSproj.App.ViewModels
{
    public class BandDetailViewModel : ViewModelBase, IBandDetailViewModel
    {
        private readonly IRepository<BandDetailModel, BandListModel> _bandRepository;
        private readonly IMediator _mediator;

        public BandDetailViewModel(IRepository<BandDetailModel, BandListModel> bandRepository, IMediator mediator)
        {
            _bandRepository = bandRepository;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
        }

        public BandWrapper? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void Load(Guid id)
        {
            Model = _bandRepository.GetById(id) ?? new BandDetailModel();
        }

        // provides logic for storing of new band
        public void Save()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved !");
            }

            Model = _bandRepository.InsertOrUpdate(Model.Model);
            _mediator.Send(new UpdateMessage<BandWrapper> {Model = Model});
        }

        // new stage can be saved only if user entered it's name, description and genre
        private bool CanSave() =>
            Model != null
            && !string.IsNullOrWhiteSpace(Model.Name)
            && !string.IsNullOrWhiteSpace(Model.Description)
            && !string.IsNullOrWhiteSpace(Model.Genre);

        // provides logic for deleting of stage
        public void Delete()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be deleted !");
            }

            if (Model.Id != Guid.Empty)
            {
                if (!(_bandRepository.Delete(Model.Id)))
                {
                    // maybe use dialog window to report message (if it will be implemented)
                    throw new OperationCanceledException("Failed to delete model !");
                }

                _mediator.Send(new DeleteMessage<BandWrapper>
                {
                    Model = Model
                });
            }
        }

        /*
        public override void LoadInDesignMode()
        {
           //Use in case designer wants to put some extern data in design time
        }
        */
    }
}