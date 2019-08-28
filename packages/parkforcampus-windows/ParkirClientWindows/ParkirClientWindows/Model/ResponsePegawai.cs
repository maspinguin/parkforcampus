using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponsePegawai: ResponseModel
    {
        public List<ResponsePegawaiList> data { get; set; }
    }
}
