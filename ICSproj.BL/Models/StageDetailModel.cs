using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSproj.BL.Models
{
    public class StageDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PhotoDetailModel> Photos { get; set; }
        public List<ScheduleDetailModel> Schedule { get; set; } 
    }
}
