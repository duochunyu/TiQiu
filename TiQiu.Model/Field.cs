using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiQiu.Model
{
    public class Field
    {
        public int ID { get; set; }        
        public int BusinessesID {get;set;}
        public string AreaCode { get; set; }
        public string AreaText { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> B { get; set; }
        public string Name { get; set; }
        public string Brief { get; set; }
        public Nullable<int> Level { get; set; }
        public string Adress { get; set; }
        public int Score { get; set; }
        public decimal BottomPrice { get; set; }
        public decimal TopPrice { get; set; }
        public bool HasBathroom { get; set; }
        public int Status { get; set; }
        public string TEL { get; set; }
        public string Phone { get; set; }
        public string PicPath { get; set; }
        public int Type { get; set; }
        public List<FieldItem> Items { get; set; }
        public List<File> Images { get; set; }

    }

    public class FieldItem
    {
        public int ID { get; set; }
        public int FieldID { get; set; }
        public int BusinessesID { get; set; }
        public int Level { get; set; }
        public int Type { get; set; }
        public string Brief { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
    }

    
}