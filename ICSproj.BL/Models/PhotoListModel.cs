using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSproj.BL.Models
{
    public class PhotoListModel : ModelBase
    {
        public byte[] Photo { get; set; }
        public Guid ForeignGuid { get; set; }
    }
}
