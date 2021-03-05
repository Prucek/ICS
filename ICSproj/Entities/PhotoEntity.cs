using System;
using System.Drawing;
using System.Net.Mime;
using System.Text;

namespace ICSproj.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public string SrcPath { get; set; } /* Todo: Refactor to filesystem-path-optimized struct/object type */

        //public Image Image { get; set; } ??instead??
        //Image image1 = Image.FromFile("c:\\FakePhoto1.jpg");

        //https://www.entityframeworktutorial.net/Types-of-Entities.aspx
        // byte[]

        //public string Extension { get; set; }
        //public double Size { get; set; }
    }
}
