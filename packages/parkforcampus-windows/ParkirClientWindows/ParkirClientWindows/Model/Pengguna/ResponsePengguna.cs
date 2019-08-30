using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponsePengguna: ResponseModel
    {
        public List<ResponsePenggunaList> data { get; set; }
    }
}
