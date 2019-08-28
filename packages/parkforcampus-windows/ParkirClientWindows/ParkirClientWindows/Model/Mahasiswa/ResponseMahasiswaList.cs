using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseMahasiswaList: ResponseModel
    {
        public List<ResponseMahasiswaDetail> data { get; set; }

    }
}
