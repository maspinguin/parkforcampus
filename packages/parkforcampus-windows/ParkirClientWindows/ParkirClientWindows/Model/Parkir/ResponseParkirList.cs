using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseParkirList: ResponseModel
    {
        public List<ResponseParkirDetail> data { get; set; }

    }
}
