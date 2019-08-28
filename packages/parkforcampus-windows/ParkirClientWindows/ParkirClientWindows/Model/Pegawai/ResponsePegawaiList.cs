using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponsePegawaiList: ResponseModel
    {
        public List<ResponsePegawaiDetail> data { get; set; }

    }
}
