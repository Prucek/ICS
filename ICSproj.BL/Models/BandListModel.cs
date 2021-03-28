using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSproj.BL.Models
{
    public class BandListModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PhotoListModel> Photos { get; set; }
        public List<ScheduleListModel> Schedule { get; set; }
    }
}
