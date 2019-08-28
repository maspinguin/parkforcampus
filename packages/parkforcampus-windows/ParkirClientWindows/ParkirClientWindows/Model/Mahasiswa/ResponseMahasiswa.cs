using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows.Model
{
    class ResponseMahasiswa: ResponseModel
    {
        public List<ResponseMahasiswaList> data { get; set; }
    }
}
