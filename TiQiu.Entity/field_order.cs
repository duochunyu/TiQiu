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
    
    public partial class field_order
    {
        public int ID { get; set; }
        public int FIELD_SCHEDULED_ID { get; set; }
        public int FIELD_ID { get; set; }
        public int FIELD_ITEM_ID { get; set; }
        public System.DateTime ORDER_DATE { get; set; }
        public bool NEED_REFEREE { get; set; }
        public int STATUS { get; set; }
        public System.TimeSpan START_TIME { get; set; }
        public System.TimeSpan END_TIME { get; set; }
        public int MEMBER_ID { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public Nullable<System.DateTime> CONFIRM_MEMBER_DATE { get; set; }
        public Nullable<int> CONFIRM_BUSINESSES_ID { get; set; }
        public Nullable<System.DateTime> CONFIRM_BUSINESSES_DATE { get; set; }
        public Nullable<System.DateTime> ARRIVE_DATE { get; set; }
        public Nullable<System.DateTime> CANCEL_DATE { get; set; }
        public Nullable<bool> IS_PK { get; set; }
        public Nullable<int> TEAM_ID { get; set; }
        public Nullable<decimal> INCOME { get; set; }
        public Nullable<int> SCORE { get; set; }
        public string REMARK { get; set; }
    }
}
