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
    
    public partial class GAME_TEAM
    {
        public int ID { get; set; }
        public int GAME_ID { get; set; }
        public int ROUND_ID { get; set; }
        public int GROUP_ID { get; set; }
        public int TEAM_ID { get; set; }
        public string TEAM_NAME { get; set; }
        public int SCORE { get; set; }
        public int RANK { get; set; }
        public int GOAL { get; set; }
        public int CONCEDED { get; set; }
        public int WIN { get; set; }
        public int LOSE { get; set; }
        public int DRAW { get; set; }
        public int ROUND { get; set; }
        public int GAMES { get; set; }
    
        public virtual GAME_ROUND_GROUP GAME_ROUND_GROUP { get; set; }
    }
}
