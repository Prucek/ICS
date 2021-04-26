﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.App.Wrappers
{
    // Wrapper je niečo ako medzivrstva medzi modelmi z BL a View modelmi z tejto fazy
    // Zabezpečuje mapovanie medzi týmito dvoma typmi modelov
    // Uľahčuje View modelom jednoduchšie prepisovať to čo vidí užívateľ

    public class BandWrapper : ModelWrapper<BandDetailModel>
    {
        public BandWrapper(BandDetailModel model)
            : base(model)
        {
            InitializePhotoDetailModelList(model);
            InitializeScheduleDetailModelList(model);
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Description
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Genre
        {
            get => GetValue<string>(); 
            set => SetValue(value);
        }

        public string OriginCountry
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string DescriptionLong
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        private void InitializePhotoDetailModelList(BandDetailModel model)
        {
            if (model.Photos == null)
            {
                // List of photos can not be empty
                return;
            }
            Photos = new ObservableCollection<PhotoWrapper>(
                model.Photos.Select(e => new PhotoWrapper(e)));

            RegisterCollection(Photos, model.Photos);
        }

        private void InitializeScheduleDetailModelList(BandDetailModel model)
        {
            if (model.Schedule == null)
            {
                // List of photos can not be empty
                return;
            }
            Schedule = new ObservableCollection<ScheduleWrapper>(
                model.Schedule.Select(e => new ScheduleWrapper(e)));

            RegisterCollection(Schedule, model.Schedule);
        }

        public ObservableCollection<PhotoWrapper> Photos { get; set; } = null!; 
        public ObservableCollection<ScheduleWrapper> Schedule { get; set; } = null!;

        public static implicit operator BandWrapper(BandDetailModel detailModel)
            => new(detailModel);

        public static implicit operator BandDetailModel(BandWrapper wrapper)
            => wrapper.Model;
    }
}
