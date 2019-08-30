using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponsePenggunaList: ResponseModel
    {
        public List<ResponsePenggunaDetail> data { get; set; }

    }
}
