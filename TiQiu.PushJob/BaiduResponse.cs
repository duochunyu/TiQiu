using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiQiu.PushJob
{
    class BaiduResponse
    {

        //   “request_id”:12394838223,
        //   “error_code”:30000,
        //   “error_msg”:”Request params not valid”
        //}
        public string request_id { get; set; }
        public string error_code { get; set; }
        public string error_msg { get; set; }
    }
}
