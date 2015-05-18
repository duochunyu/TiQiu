using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiQiu.PushJob
{
    class CustomMessage
    {
        public int MessageType { get; set; }

        public string Content { get; set; }

        public string PkId { get; set; }

        public string LinkUrl { get; set; }
    }
}
