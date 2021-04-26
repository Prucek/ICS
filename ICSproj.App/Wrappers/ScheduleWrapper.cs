using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.App.Wrappers
{
    // Wrapper je niečo ako medzivrstva medzi modelmi z BL a View modelmi z tejto fazy
    // Zabezpečuje mapovanie medzi týmito dvoma typmi modelov
    // Uľahčuje View modelom jednoduchšie prepisovať to čo vidí užívateľ

    public class ScheduleWrapper : ModelWrapper<ScheduleDetailModel>
    {
        public ScheduleWrapper(ScheduleDetailModel model)
            : base(model)
        { }

        public Guid BandId
        {
            get => GetValue<Guid>(); 
            set => SetValue(value);
        }

        public string BandName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string BandDescription
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public Guid StageId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public string StageName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string StageDescription
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime? PerformanceDateTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public TimeSpan PerformanceDuration
        {
            get => GetValue<TimeSpan>();
            set => SetValue(value);
        }

        public static implicit operator ScheduleWrapper(ScheduleDetailModel detailModel)
            => new(detailModel);

        public static implicit operator ScheduleDetailModel(ScheduleWrapper wrapper)
            => wrapper.Model;
    }
}
