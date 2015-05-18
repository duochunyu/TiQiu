using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class FieldWeekRule
    {
        public int ID { get; set; }
        public int FieldId { get; set; }
        public int FieldItemId { get; set; }
        public int FieldType { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int DayOfWeek {get;set;}
        public string Description { get; set; }
    }
}