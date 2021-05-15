﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private readonly IRepository<BandDetailModel, BandListModel> _bandRepository;
        private readonly IRepository<StageDetailModel, StageListModel> _stageRepository;

        private readonly IMediator _mediator;

        public ScheduleDetailViewModel(IRepository<ScheduleDetailModel, ScheduleListModel> scheduleRepository, 
                                    IRepository<BandDetailModel, BandListModel> bandRepository,
                                    IRepository<StageDetailModel, StageListModel> stageRepository,
        IMediator mediator)
        {
            _scheduleRepository = scheduleRepository;
            _bandRepository = bandRepository;
            _stageRepository = stageRepository;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete);

            //_mediator.Register<SelectedMessage<ScheduleWrapper>>(LoadSchedule);
        }

        //private void LoadSchedule(SelectedMessage<ScheduleWrapper> msg)
        //{
        //    var SelectedSchedule = _scheduleRepository.GetById(msg.Id);
        //    if (SelectedStage != null && SelectedBand != null)
        //    {
        //        SelectedBand.Name = SelectedSchedule.BandName;
        //        SelectedStage.Name = SelectedSchedule.StageName;
        //        DateTimeStart = SelectedSchedule.PerformanceDateTime;
        //        TimeSpan = SelectedSchedule.PerformanceDuration;
        //    }
        //}

        public ScheduleWrapper? Model { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICollection<BandListModel> BandCollection { get; set; }
        public ICollection<StageListModel> StageCollection { get; set; }

        //public BandListModel SelectedBand { get; set; } = new BandListModel();

        //public StageListModel SelectedStage { get; set; } = new StageListModel();

        //public DateTime DateTimeStart { get; set; } = DateTime.Today;
        //public TimeSpan TimeSpan { get; set; } = TimeSpan.FromHours(2);

        public void Load(Guid id)
        {
            Model = _scheduleRepository.GetById(id) ?? new ScheduleDetailModel();
            if (Model == null)
            {
                Model.PerformanceDateTime = DateTime.Today;
                Model.PerformanceDuration = TimeSpan.FromHours(2);
            }
            BandCollection = _bandRepository.GetAll();
            StageCollection = _stageRepository.GetAll();
        }

        // provides logic for storing of new schedule
        public void Save()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved !");
            }

            var result = _scheduleRepository.InsertOrUpdate(Model.Model);
            if (result == null)
            {
                MessageBox.Show("Time collision when inserting new model !");
                //return;
            }

            _mediator.Send(new UpdateMessage<ScheduleWrapper> { Model = Model });
        }

        // new schedule can not be saved without name of band, stage, performance duration and date
        private bool CanSave() =>
            Model != null
            && !string.IsNullOrWhiteSpace(Model.BandName)
            && !string.IsNullOrWhiteSpace(Model.StageName);

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
