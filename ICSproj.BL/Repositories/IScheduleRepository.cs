using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.BL.Repositories
{
    public interface IScheduleRepository
    {
        IEnumerable<ScheduleListModel> GetAll();
        ScheduleDetailModel GetById(Guid id);
        ScheduleDetailModel InsertOrUpdate(ScheduleDetailModel model);
        void Delete(Guid id);
    }
}
