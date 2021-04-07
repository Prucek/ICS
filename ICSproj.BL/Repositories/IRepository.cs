using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.BL.Repositories
{
    public interface IRepository<TDetailModel, TListModel>
    {
        IEnumerable<TListModel> GetAll();
        TDetailModel GetById(Guid id);
        TDetailModel InsertOrUpdate(TDetailModel model);
        bool Delete(Guid id);
    }
}
