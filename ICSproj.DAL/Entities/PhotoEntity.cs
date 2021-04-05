using System;
using System.Drawing;
using System.Net.Mime;
using System.Text;

namespace ICSproj.DAL.Entities
{
    public class PhotoEntity : BaseEntity
    {
        public byte[] Photo { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
    }
}
