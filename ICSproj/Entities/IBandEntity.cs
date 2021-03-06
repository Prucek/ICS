﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ICSproj.Entities
{
    public interface IBandEntity : IFestivalEntity
    {
        public string Genre { get; set; }
        public string Origin { get; set; }
        public string DescriptionLong { get; set; }
    }
}