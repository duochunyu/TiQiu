//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiQiu.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class field
    {
        public int ID { get; set; }
        public int BUSINESSES_ID { get; set; }
        public string AREA_CODE { get; set; }
        public Nullable<float> L { get; set; }
        public Nullable<float> B { get; set; }
        public string NAME { get; set; }
        public string BRIEF { get; set; }
        public Nullable<int> LEVEL { get; set; }
        public int HALF_SIZE_FIELD { get; set; }
        public int FULL_SIZE_FIELD { get; set; }
        public string ADRESS { get; set; }
        public int SCORE { get; set; }
        public decimal BOTTOM_PRICE { get; set; }
        public decimal TOP_PRICE { get; set; }
        public bool HAS_BATHROOM { get; set; }
        public System.TimeSpan END_TIME { get; set; }
        public System.TimeSpan START_TIME { get; set; }
    }
}
