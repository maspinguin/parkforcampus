﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseBy
    {
        public DateTime created_at { get; set; }
        public int created_by { get; set; }
        public DateTime updated_at { get; set; }
        public int updated_by { get; set; }
    }
}
