//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiQiu.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class FIELD
    {
        public FIELD()
        {
            this.FIELD_ITEM = new HashSet<FIELD_ITEM>();
        }
    
        public int ID { get; set; }
        public int BUSINESSES_ID { get; set; }
        public string AREA_CODE { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> B { get; set; }
        public string NAME { get; set; }
        public string BRIEF { get; set; }
        public Nullable<int> LEVEL { get; set; }
        public string ADRESS { get; set; }
        public int SCORE { get; set; }
        public decimal BOTTOM_PRICE { get; set; }
        public decimal TOP_PRICE { get; set; }
        public bool HAS_BATHROOM { get; set; }
        public int STATUS { get; set; }
        public string TEL { get; set; }
        public string PHONE { get; set; }
        public string PIC_PATH { get; set; }
        public int TYPE { get; set; }
    
        public virtual ICollection<FIELD_ITEM> FIELD_ITEM { get; set; }
        public virtual BUSINESSES BUSINESSES { get; set; }
    }
}
