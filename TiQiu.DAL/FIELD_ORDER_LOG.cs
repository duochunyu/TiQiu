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
    
    public partial class FIELD_ORDER_LOG
    {
        public int ID { get; set; }
        public int FIELD_ORDER_ID { get; set; }
        public int FIELD_ITEM_ID { get; set; }
        public string OPERATION { get; set; }
        public Nullable<int> MEMBER_ID { get; set; }
        public Nullable<int> ACCOUNT_B_ID { get; set; }
        public System.DateTime LOG_DATE { get; set; }
        public int ORDER_STATUS { get; set; }
    
        public virtual FIELD_ORDER FIELD_ORDER { get; set; }
    }
}
