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
    
    public partial class sms_confirm
    {
        public int ID { get; set; }
        public int SMS_POOL_ID { get; set; }
        public string CELL_PHONE { get; set; }
        public string CONTENT { get; set; }
        public System.DateTime RECEIVE_DATE { get; set; }
    }
}
