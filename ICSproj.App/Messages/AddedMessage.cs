﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;

namespace ICSproj.App.Messages
{
    public class AddedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
