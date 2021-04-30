using ICSproj.App.Services;
using ICSproj.BL.Repositories;
using System;
using System.Windows.Input;
using ICSproj.App.Commands;
using ICSproj.App.Messages;
using ICSproj.App.ViewModels.Interfaces;
using ICSproj.App.Wrappers;
using ICSproj.BL.Models;

namespace ICSproj.App.ViewModels
{
    public class StageDetailViewModel : ViewModelBase, IStageDetailViewModel
    {
        private readonly IRepository<StageDetailModel, StageListModel> _stageRepository;
        private readonly IMediator _mediator;

        public StageDetailViewModel(IRepository<StageDetailModel, StageListModel> stageRepository, IMediator mediator)
        {
            _stageRepository = stageRepository;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
        }

        public StageWrapper? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void Load(Guid id)
        {
            Model = _stageRepository.GetById(id) ?? new StageDetailModel();
        }

        // provides logic for storing of new stage 
        public void Save()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved !");
            }

            Model = _stageRepository.InsertOrUpdate(Model.Model);
            _mediator.Send(new UpdateMessage<StageWrapper> { Model = Model });
        }

        // new stage can be saved only if user entered it's name and description
        private bool CanSave() =>
            Model != null
            && !string.IsNullOrWhiteSpace(Model.Name)
            && !string.IsNullOrWhiteSpace(Model.Description);

        // provides logic for deleting of stage
        public void Delete()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be deleted !");
            }

            if (Model.Id != Guid.Empty)
            {
                if (!(_stageRepository.Delete(Model.Id)))
                {
                    // maybe use dialog window to report message (if it will be implemented)
                    throw new OperationCanceledException("Failed to delete model !");
                }

                _mediator.Send(new DeleteMessage<StageWrapper>
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
