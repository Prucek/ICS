using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSproj.BL.Models
{
    public class PhotoDetailModel : ModelBase
    {
        public byte[] Photo { get; set; }
        public Guid ForeignGuid { get; set; }
    }
}
