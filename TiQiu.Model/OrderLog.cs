using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class OrderLog
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public int FieldId { get; set; }
        public int FieldItemId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string Message { get; set; }
        public System.DateTime LogDate { get; set; }
    }
}