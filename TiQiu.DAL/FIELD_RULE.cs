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
    
    public partial class FIELD_RULE
    {
        public int ID { get; set; }
        public int FIELD_ID { get; set; }
        public int FIELD_ITEM_ID { get; set; }
        public int TYPE { get; set; }
        public int FIELD_TYPE { get; set; }
        public int SCHEDULE_TYPE { get; set; }
        public System.DateTime START_DATE { get; set; }
        public System.DateTime END_DATE { get; set; }
        public System.TimeSpan START_TIME { get; set; }
        public System.TimeSpan END_TIME { get; set; }
        public string RULE_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public decimal PRICE { get; set; }
        public int STATUS { get; set; }
    }
}
