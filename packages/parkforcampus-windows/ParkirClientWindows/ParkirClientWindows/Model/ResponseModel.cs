using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseModel
    {
        public int status { get; set; }
        public bool valid { get; set; }
        public string message { get; set; }
    }
}
