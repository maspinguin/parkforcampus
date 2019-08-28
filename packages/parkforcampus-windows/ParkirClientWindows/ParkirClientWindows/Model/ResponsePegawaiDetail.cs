using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponsePegawaiDetail : ResponseBy
    {
        public int id { get; set; }
        public int status_id { get; set; }
        public string nip { get; set; }
        public string alamat { get; set; }
        public string email { get; set; }
        public string nama { get; set; }

    }
}
