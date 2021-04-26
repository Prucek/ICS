using System;
using System.Collections.Generic;
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
    public class ScheduleDetailViewModel : ViewModelBase, IScheduleDetailViewModel
    {
        private readonly IRepository<ScheduleDetailModel, ScheduleListModel> _scheduleRepository;
        private readonly IMediator _mediator;

        public ScheduleDetailViewModel(IRepository<ScheduleDetailModel, ScheduleListModel> scheduleRepository,
            IMediator mediator)
        {
            _scheduleRepository = scheduleRepository;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);
        }

        public ScheduleWrapper? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public void Load(Guid id)
        {
            Model = _scheduleRepository.GetById(id) ?? new ScheduleDetailModel();
        }

        // provides logic for storing of new schedule
        public void Save()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved !");
            }

            Model = _scheduleRepository.InsertOrUpdate(Model.Model);
            if (Model == null)
            {
                // collision in time stamp of concerts...maybe we should notify user with dialog message
                throw new InvalidOperationException("Time collision when inserting new model !");
            }

            _mediator.Send(new UpdateMessage<ScheduleWrapper> { Model = Model });
        }

        // new schedule can not be saved without name of band, stage, performance duration and date
        private bool CanSave() =>
            Model != null
            && !string.IsNullOrWhiteSpace(Model.BandName)
            && !string.IsNullOrWhiteSpace(Model.StageName)
            && Model.PerformanceDuration != default
            && Model.PerformanceDateTime != null;

        // provides logic for deleting of schedule
        public void Delete()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be deleted !");
            }

            if (Model.Id != Guid.Empty)
            {
                if (!(_scheduleRepository.Delete(Model.Id)))
                {
                    // maybe use dialog window to report message (if it will be implemented)
                    throw new OperationCanceledException("Failed to delete model !");
                }

                _mediator.Send(new DeleteMessage<ScheduleWrapper>
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
