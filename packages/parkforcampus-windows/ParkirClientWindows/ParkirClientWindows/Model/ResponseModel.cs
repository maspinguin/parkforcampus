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
        public int total { get; set; }
        public bool valid { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string nama { get; set; }
        public string nomor_induk { get; set; }
        public int id_type { get; set; }

        

    }
 
}
