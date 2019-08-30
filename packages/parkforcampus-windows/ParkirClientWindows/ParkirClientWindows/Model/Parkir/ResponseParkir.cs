using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseParkir: ResponseModel
    {
        public List<ResponseParkirList> data { get; set; }
    }
}
