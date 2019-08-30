using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseParkirDetail : ResponseBy
    {
        public int id { get; set; }
        public int status_id { get; set; }
        public string nomor_induk { get; set; }
        public string jenis_parkir { get; set; }
       
        public string waktu { get; set; }

    }
}
