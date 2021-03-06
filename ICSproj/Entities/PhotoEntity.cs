using System;
using System.Drawing;
using System.Net.Mime;
using System.Text;

namespace ICSproj.Entities
{
    public class PhotoEntity : BaseEntity
    {
        private byte[] Photo { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
    }
}
