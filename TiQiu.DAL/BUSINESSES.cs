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
    
    public partial class BUSINESSES
    {
        public BUSINESSES()
        {
            this.FIELD = new HashSet<FIELD>();
            this.FIELD_ITEM = new HashSet<FIELD_ITEM>();
        }
    
        public int ID { get; set; }
        public int LEVEL { get; set; }
        public int STATUS { get; set; }
        public string NAME { get; set; }
        public string BRIEF { get; set; }
    
        public virtual ICollection<FIELD> FIELD { get; set; }
        public virtual ICollection<FIELD_ITEM> FIELD_ITEM { get; set; }
    }
}
