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
    
    public partial class MEMBER
    {
        public MEMBER()
        {
            this.ACCOUNT = new HashSet<ACCOUNT>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public bool IS_TEAMLEADER { get; set; }
        public string CELLPHONE { get; set; }
        public string EMAIL { get; set; }
        public string ADRESS { get; set; }
        public string AREA_CODE { get; set; }
        public int LEVLE { get; set; }
        public Nullable<int> SCORE { get; set; }
        public string NICK_NAME { get; set; }
        public string POSITION { get; set; }
        public string FAV_FOOT { get; set; }
        public string FAV_TEAM { get; set; }
        public string FAV_STAR { get; set; }
        public string BRIEF { get; set; }
        public string WORK { get; set; }
        public string INTRO { get; set; }
        public Nullable<System.DateTime> BRITHDAY { get; set; }
        public Nullable<System.DateTime> PLAY_AGE { get; set; }
        public string FEATURE { get; set; }
        public string TITLE { get; set; }
        public Nullable<int> SEX { get; set; }
    
        public virtual ICollection<ACCOUNT> ACCOUNT { get; set; }
    }
}
