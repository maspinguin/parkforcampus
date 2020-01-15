using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseRekapParkirList: ResponseModel
    {
        public List<ResponseRekapParkirDetail> data { get; set; }
    }
}
