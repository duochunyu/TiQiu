using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class File
    {
        public int ID { get; set; }
        public string FileUrl { get; set; }
        public string Title { get; set; }        
        public string LinkUrl { get; set; }
        public int OptType { get; set; }
        public int PkId { get; set; }
        public int Order { get; set; }
    }
}