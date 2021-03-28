using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICSproj.BL.Models
{
    public class PhotoDetailModel : ModelBase
    {
        public byte[] Photo { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
    }
}
