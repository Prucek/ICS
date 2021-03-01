using System;
using System.Collections.Generic;
using System.Text;

namespace ICSproj.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public string SrcPath { get; set; } /* Todo: Refactor to filesystem-path-optimized struct/object type */

        //public string Extension { get; set; }
        //public double Size { get; set; }
    }
}
