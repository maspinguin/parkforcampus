﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseRekapParkirDetail : ResponseBy
    {
       public int jumlah { get; set; }

       public string hour { get; set; }
    }
}
