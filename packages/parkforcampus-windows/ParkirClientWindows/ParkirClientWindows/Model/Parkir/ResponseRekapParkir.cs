using ParkirClientWindows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseRekapParkir: ResponseModel
    {
        public List<ResponseRekapParkirList> data { get; set; }
        public string jam_ramai { get; set; }
    }
}
